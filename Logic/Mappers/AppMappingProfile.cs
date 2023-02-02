using AutoMapper;
using Data.DTO;
using Data.DTO.Auth;
using Data.DTO.Review;
using Data.DTO.Shop;
using Data.DTO.User;
using Data.Entities;
using Data.Models;

namespace WebAPi.Mappers
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<RegistrationDTO, User>();

            CreateMap<Shop, ShopDTO>();
            CreateMap<PageModel<Shop>, PageModel<ShopDTO>>();
            CreateMap<CreateShopDTO, Shop>();
            CreateMap<UpdateShopDTO, Shop>();

            CreateMap<Review, ReviewDTO>();
            CreateMap<PageModel<Review>, PageModel<ReviewDTO>>();
            CreateMap<CreateReviewDTO, Review>();
            CreateMap<UpdateReviewDTO, Review>();

            CreateMap<User, UserDTO>();
            CreateMap<PageModel<User>, PageModel<UserDTO>>();
            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();

            CreateMap<ChangePasswordDTO, User>().ReverseMap();
            CreateMap<CreateAdminModel, User>().ReverseMap();

            CreateMap<Shop, ShopDTO>().ForMember(x => x.ImagePath, opt => opt.Ignore());

            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>();
            CreateMap<PageModel<Category>, PageModel<CategoryDTO>>();
            CreateMap<Data.Entities.Type, TypeDTO>().ReverseMap();
            CreateMap<PageModel<Data.Entities.Type>, PageModel<TypeDTO>>();
            CreateMap<DeliveryType, CreateDeliveryTypeDTO>().ReverseMap();
            CreateMap<PageModel<DeliveryType>, PageModel<CreateDeliveryTypeDTO>>();
            CreateMap<PaymentMethod, PaymentMethodDTO>().ReverseMap();
            CreateMap<PageModel<PaymentMethod>, PageModel<PaymentMethodDTO>>();
        }
    }
}