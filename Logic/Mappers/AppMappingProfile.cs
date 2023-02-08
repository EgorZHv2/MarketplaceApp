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

namespace WebAPi.Mappers
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<RegistrationDTO, User>();

           
            CreateMap<Shop, ShopDTO>().ForMember(x => x.ImagePath, opt => opt.Ignore());;
            CreateMap<PageModelDTO<Shop>, PageModelDTO<ShopDTO>>();
            CreateMap<CreateShopDTO, Shop>()
                .ForMember(e => e.ShopPaymentMethods, opt => opt.Ignore())
                .ForMember(e => e.ShopDeliveryTypes, opt => opt.Ignore());
       
            CreateMap<UpdateShopDTO, Shop>()
                .ForMember(e => e.ShopPaymentMethods, opt => opt.Ignore())
                .ForMember(e => e.ShopDeliveryTypes, opt => opt.Ignore());

            CreateMap<Review, ReviewDTO>();
            CreateMap<PageModelDTO<Review>, PageModelDTO<ReviewDTO>>();
            CreateMap<CreateReviewDTO, Review>();
            CreateMap<UpdateReviewDTO, Review>();

            CreateMap<User, UserDTO>();
            CreateMap<PageModelDTO<User>, PageModelDTO<UserDTO>>();
            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();

            CreateMap<ChangePasswordDTO, User>().ReverseMap();
            CreateMap<CreateAdminDTO, User>().ReverseMap();

            

            CreateMap<Category, CategoryDTO>();
            CreateMap<PageModelDTO<Category>, PageModelDTO<CategoryDTO>>();
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();

            CreateMap<DeliveryType, DeliveryTypeDTO>();
            CreateMap<PageModelDTO<DeliveryType>, PageModelDTO<DeliveryTypeDTO>>();
            CreateMap<CreateDeliveryTypeDTO, DeliveryType>();
            CreateMap<UpdateDeliveryTypeDTO, DeliveryType>();

            CreateMap<PaymentMethod, PaymentMethodDTO>();
            CreateMap<PageModelDTO<PaymentMethod>, PageModelDTO<PaymentMethodDTO>>();
            CreateMap<CreatePaymentMethodDTO, PaymentMethod>();
            CreateMap<UpdatePaymentMethodDTO, PaymentMethod>();

            CreateMap<Data.Entities.Type, TypeDTO>();
            CreateMap<PageModelDTO<Data.Entities.Type>, PageModelDTO<TypeDTO>>();
            CreateMap<CreateTypeDTO, Data.Entities.Type>();
            CreateMap<UpdateTypeDTO, Data.Entities.Type>();
        }
    }
}