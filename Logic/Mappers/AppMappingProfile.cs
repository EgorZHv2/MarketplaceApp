using AutoMapper;
using Data.DTO;
using Data.DTO.Auth;
using Data.DTO.Category;
using Data.DTO.DeliveryType;
using Data.DTO.PaymentMethod;
using Data.DTO.Review;
using Data.DTO.Shop;
using Data.DTO.Type;
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
            CreateMap<CreateAdminDTO, User>().ReverseMap();

            CreateMap<Shop, ShopDTO>().ForMember(x => x.ImagePath, opt => opt.Ignore());

            CreateMap<Category, CategoryDTO>();
            CreateMap<PageModel<Category>, PageModel<CategoryDTO>>();
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();

            CreateMap<DeliveryType, DeliveryTypeDTO>();
            CreateMap<PageModel<DeliveryType>, PageModel<DeliveryTypeDTO>>();
            CreateMap<CreateDeliveryTypeDTO, DeliveryType>();
            CreateMap<UpdateDeliveryTypeDTO, DeliveryType>();

            CreateMap<PaymentMethod, PaymentMethodDTO>();
            CreateMap<PageModel<PaymentMethod>, PageModel<PaymentMethodDTO>>();
            CreateMap<CreatePaymentMethodDTO, PaymentMethod>();
            CreateMap<UpdatePaymentMethodDTO, PaymentMethod>();

            CreateMap<Data.Entities.Type, TypeDTO>();
            CreateMap<PageModel<Data.Entities.Type>, PageModel<TypeDTO>>();
            CreateMap<CreateTypeDTO,Data.Entities.Type>();
            CreateMap<UpdateTypeDTO, Data.Entities.Type>();
        }
    }
}