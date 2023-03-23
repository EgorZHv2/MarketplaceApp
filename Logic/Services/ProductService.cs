using AutoMapper;
using Data.DTO.Filters;
using Data.DTO;
using Data.DTO.Product;
using Data.DTO.Shop;
using Data.Entities;
using Data.IRepositories;
using Data.Repositories.Repositories;
using Logic.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Exceptions;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Data.DTO.Category;
using Data.Enums;
using Microsoft.Extensions.Options;
using Data.Options;
using System.Net;
using System.IO;

namespace Logic.Services
{
    public class ProductService
        : BaseService<
            ProductEntity,
            ProductDTO,
            CreateProductDTO,
            UpdateProductDTO,
            IProductRepository
        >,
            IProductService
    {

        private ICategoryRepository _categoryRepository;
        private IImageService _imageService;
        private ICategoryService _categoryService;
        private ApplicationOptions _options;
        private IFileHttpService _fileHttpService;

        public ProductService(
            IProductRepository repository,
            IMapper mapper,
            ICategoryRepository categoryRepository,
            IImageService imageService,
            ICategoryService categoryService,
            IOptions<ApplicationOptions> options,
            IFileHttpService fileHttpService
        ) : base(repository, mapper)
        {

            _categoryRepository = categoryRepository;
            _imageService = imageService;
            _categoryService = categoryService;
            _options = options.Value;
            _fileHttpService = fileHttpService;
        }

        public async Task<PageModelDTO<ProductDTO>> GetPage(
            PaginationDTO pagingModel,
            ProductFilterDTO filter
        )
        {
            var pages = await _repository.GetPage(pagingModel, filter);

            var result = _mapper.Map<PageModelDTO<ProductDTO>>(pages);

            return result;
        }

        public async Task<PageModelDTO<ProductDTOWithPrice>> GetProductsInShopsPage(
            PaginationDTO pagingModel,
            ShopProductFilterDTO filter
        )
        {
            var pages = await _repository.GetProductsInShopsPage(pagingModel, filter);

            var result = _mapper.Map<PageModelDTO<ProductDTOWithPrice>>(pages);

            return result;
        }

        public override async Task<Guid> Create(CreateProductDTO createDTO)
        {
            var category = _categoryRepository.GetById(createDTO.CategoryId);
            if (category == default)
            {
                throw new CategoryNotFoundException();
            }
            var result = await base.Create(createDTO);
            await _imageService.CreateManyImages(createDTO.Photos, result);
            return result;
        }

        public override async Task Update(UpdateProductDTO DTO)
        {
            var category = _categoryRepository.GetById(DTO.CategoryId);
            if (category == default)
            {
                throw new CategoryNotFoundException();
            }
            await base.Update(DTO);
        }

        public async Task AddProductsFromExcelFile(IFormFile excelFile)
        {
            var list = new List<ExcelProductModel>();
            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    int categoriesTiers = 0;
                    Dictionary<int, ProductExcelColumns> KeyColumns =
                        new Dictionary<int, ProductExcelColumns>();
                    int columns = 0;
                    for (int column = 1; column < worksheet.Dimension.Columns; column++)
                    {
                       if(string.IsNullOrEmpty(worksheet.Cells[1, column].Value?.ToString().Trim()))
                        {
                            columns = --column;
                            break;
                            
                        }
                       switch (worksheet.Cells[1, column].Value.ToString().Trim())
                        {
                            case string e when e.Contains(ProductExcelColumns.Каталог.ToString()):
                                categoriesTiers++;
                                if (categoriesTiers > _options.MaxCategoryTier)
                                {
                                    throw new CategoryTierException(_options.MaxCategoryTier);
                                }
                                KeyColumns.Add(column, ProductExcelColumns.Каталог);
                                break;

                            case string e when e.Contains(ProductExcelColumns.Код.ToString()):
                                KeyColumns.Add(column, ProductExcelColumns.Код);
                                break;

                            case string e when e.Contains(ProductExcelColumns.Название.ToString()):
                                KeyColumns.Add(column, ProductExcelColumns.Название);
                                break;

                            case string e when e.Contains(ProductExcelColumns.Описание.ToString()):
                                KeyColumns.Add(column, ProductExcelColumns.Описание);
                                break;

                            case string e when e.Contains(ProductExcelColumns.Вес.ToString()):
                                KeyColumns.Add(column, ProductExcelColumns.Вес);
                                break;

                            case string e when e.Contains(ProductExcelColumns.Ширина.ToString()):
                                KeyColumns.Add(column, ProductExcelColumns.Ширина);
                                break;

                            case string e when e.Contains(ProductExcelColumns.Высота.ToString()):
                                KeyColumns.Add(column, ProductExcelColumns.Высота);
                                break;

                            case string e when e.Contains(ProductExcelColumns.Глубина.ToString()):
                                KeyColumns.Add(column, ProductExcelColumns.Глубина);
                                break;

                            case string e when e.Contains(ProductExcelColumns.Страна.ToString()):
                                KeyColumns.Add(column, ProductExcelColumns.Страна);
                                break;
                            case string e
                                when e.Contains(ProductExcelColumns.Фотографии.ToString()):
                                KeyColumns.Add(column, ProductExcelColumns.Фотографии);
                                break;

                            default:
                                break;
                        }
                    }

                    for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                    {
                        var entity = new ExcelProductModel();
                        for (int column = 1; column <= columns; column++)
                        { 
                            
                            switch (KeyColumns[column])
                            {
                                case ProductExcelColumns.Каталог:
                                    if (!string.IsNullOrEmpty(worksheet.Cells[row, column].Value?.ToString().Trim()))
                                    {
                                        entity.CategoriesNames.Enqueue(worksheet.Cells[row, column].Value.ToString().Trim());
                                    }
                                    break;
                                case ProductExcelColumns.Код:
                                    entity.PartNumber = int.Parse(worksheet.Cells[row, column].Value?.ToString().Trim() ?? throw new RequiredImportPropertyException(ProductExcelColumns.Код.ToString()));
                                    break;
                                case ProductExcelColumns.Название:
                                    entity.Name = worksheet.Cells[row, column].Value?.ToString().Trim() ?? throw new RequiredImportPropertyException(ProductExcelColumns.Название.ToString());
                                    break;
                                case ProductExcelColumns.Описание:
                                    entity.Description = worksheet.Cells[row, column].Value?.ToString().Trim() ?? throw new RequiredImportPropertyException(ProductExcelColumns.Описание.ToString());
                                    break;
                                case ProductExcelColumns.Вес:
                                    entity.Weight = double.Parse(worksheet.Cells[row, column].Value?.ToString().Trim() ?? throw new RequiredImportPropertyException(ProductExcelColumns.Вес.ToString()));
                                    break;
                                case ProductExcelColumns.Ширина:
                                    entity.Width = double.Parse(worksheet.Cells[row, column].Value?.ToString().Trim() ?? throw new RequiredImportPropertyException(ProductExcelColumns.Ширина.ToString()));
                                    break;
                                case ProductExcelColumns.Высота:
                                    entity.Height = double.Parse(worksheet.Cells[row, column].Value?.ToString().Trim() ?? throw new RequiredImportPropertyException(ProductExcelColumns.Высота.ToString()));
                                    break;
                                case ProductExcelColumns.Глубина:
                                    entity.Depth = double.Parse(worksheet.Cells[row, column].Value?.ToString().Trim() ?? throw new RequiredImportPropertyException(ProductExcelColumns.Глубина.ToString()));
                                    break;
                                case ProductExcelColumns.Страна:
                                    entity.Country = worksheet.Cells[row, column].Value.ToString().Trim() ?? throw new RequiredImportPropertyException(ProductExcelColumns.Страна.ToString());
                                    break;
                                case ProductExcelColumns.Фотографии:
                                    entity.Photos = worksheet.Cells[row, column].Value.ToString().Trim().Replace(" ", "").Split(";").ToList();
                                    break;
                                default:
                                    break;


                            }
                        }

                        list.Add(entity);
                    }

            }   }
            foreach (var item in list)
            {
                if (!item.CategoriesNames.Any())
                {
                    throw new CategoryNotFoundException();
                }
                Guid? parentCategoryId = null;
                while (item.CategoriesNames.Any())
                {
                    var categoryName = item.CategoriesNames.Dequeue();
                    var category = await _categoryRepository.GetCategoryByName(categoryName);
                    if (category == null)
                    {
                        parentCategoryId = await _categoryService.Create(new CreateCategoryDTO
                        {
                            Name = categoryName,
                            ParentCategoryId = parentCategoryId
                        });
                    }
                    else
                    {
                        parentCategoryId= category.Id;
                    }
                }
                var country = default(Country);
                try
                {
                    country = (Country)Enum.Parse(typeof(Country), item.Country);
                }
                catch
                {
                    throw new CountryNotFoundException();
                }
                var existingProduct = await _repository.GetByPartNumber(item.PartNumber.Value);
                var imageIds = new List<Guid>();
                var imageLinks = new List<string>();
                if (existingProduct == default)
                {
                    var product = _mapper.Map<ProductEntity>(item);
                    product.CategoryId = parentCategoryId.Value;
                    product.Country = country;
                    await _repository.Create(product);
                    foreach(var photoLink in item.Photos)
                    {
                      
                        var fileInfo = await _fileHttpService.PostFileFromLinkAsync(new CreateFileFromLinkDTO { EntityId = product.Id, FileLink = photoLink });
                        imageIds.Add(fileInfo.Id);
                        imageLinks.Add(fileInfo.FileLink);
                    }
                    product.ImagesLinks = imageLinks.ToArray();
                    product.ImagesIds = imageIds.ToArray();
                    
                    await _repository.Update(product);
                }
                else
                {
                    _mapper.Map(item, existingProduct);
                    existingProduct.CategoryId = parentCategoryId.Value;
                    existingProduct.Country = country;
                    foreach(var photoLink in item.Photos)
                    {
                                           
                        var fileInfo = await _fileHttpService.PostFileFromLinkAsync(new CreateFileFromLinkDTO { EntityId = existingProduct.Id, FileLink = photoLink });
                        imageIds.Add(fileInfo.Id);
                        imageLinks.Add(fileInfo.FileLink);
                    }
                    existingProduct.ImagesLinks = imageLinks.ToArray();
                        existingProduct.ImagesIds = imageIds.ToArray();
                    await _repository.Update(existingProduct);
                    
                }
            }
        }
    }
}
