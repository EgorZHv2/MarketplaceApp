using AutoMapper;
using Data;
using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Data.Repositories;
using Logic.Interfaces;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : BaseDictionaryController<Category,CategoryDTO>
    {
        private ILogger<CategoryController> _logger;
        private IRepositoryWrapper _repositoryWrapper;
        private IBaseDictionaryRepository<Category> _dictionaryRepository;
        private IMapper _mapper;
        private ApplicationDbContext _context;
        public CategoryController(IRepositoryWrapper repositoryWrapper,
            ILogger <CategoryController> logger,
            IMapper mapper,
            ApplicationDbContext context) 
        {
            _context = context;
            _repositoryWrapper = repositoryWrapper;
            _dictionaryRepository= new BaseDictionaryRepository<Category>(_context);
            _mapper = mapper;
            _dictionaryService= new BaseDictionaryService<Category,CategoryDTO>(_repositoryWrapper,_dictionaryRepository,_mapper);
            _logger = logger;
            _logger.LogError("Конструктор категории сработал");
            
        }
    }
}
