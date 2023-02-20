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
            CreateMap<RegistrationDTO, UserEntity>();

           
            CreateMap<ShopEntity, ShopDTO>().ForMember(x => x.ImagePath, opt => opt.Ignore());;
            CreateMap<PageModelDTO<ShopEntity>, PageModelDTO<ShopDTO>>();
            CreateMap<CreateShopDTO, ShopEntity>()
                .ForMember(e => e.ShopPaymentMethods, opt => opt.Ignore())
                .ForMember(e => e.ShopDeliveryTypes, opt => opt.Ignore());
       
            CreateMap<UpdateShopDTO, ShopEntity>()
                .ForMember(e => e.ShopPaymentMethods, opt => opt.Ignore())
                .ForMember(e => e.ShopDeliveryTypes, opt => opt.Ignore());

            CreateMap<ReviewEntity, ReviewDTO>();
            CreateMap<PageModelDTO<ReviewEntity>, PageModelDTO<ReviewDTO>>();
            CreateMap<CreateReviewDTO, ReviewEntity>();
            CreateMap<UpdateReviewDTO, ReviewEntity>();

            CreateMap<UserEntity, UserDTO>();
            CreateMap<PageModelDTO<UserEntity>, PageModelDTO<UserDTO>>();
            CreateMap<CreateUserDTO, UserEntity>();
            CreateMap<UpdateUserDTO, UserEntity>();

            CreateMap<ChangePasswordDTO, UserEntity>().ReverseMap();
            CreateMap<CreateAdminDTO, UserEntity>().ReverseMap();

            

            CreateMap<CategoryEntity, CategoryDTO>();
            CreateMap<PageModelDTO<CategoryEntity>, PageModelDTO<CategoryDTO>>();
            CreateMap<CreateCategoryDTO, CategoryEntity>();
            CreateMap<UpdateCategoryDTO, CategoryEntity>();

            CreateMap<DeliveryTypeEntity, DeliveryTypeDTO>();
            CreateMap<PageModelDTO<DeliveryTypeEntity>, PageModelDTO<DeliveryTypeDTO>>();
            CreateMap<CreateDeliveryTypeDTO, DeliveryTypeEntity>();
            CreateMap<UpdateDeliveryTypeDTO, DeliveryTypeEntity>();

            CreateMap<PaymentMethodEntity, PaymentMethodDTO>();
            CreateMap<PageModelDTO<PaymentMethodEntity>, PageModelDTO<PaymentMethodDTO>>();
            CreateMap<CreatePaymentMethodDTO, PaymentMethodEntity>();
            CreateMap<UpdatePaymentMethodDTO, PaymentMethodEntity>();

            CreateMap<TypeEntity, TypeDTO>();
            CreateMap<PageModelDTO<TypeEntity>, PageModelDTO<TypeDTO>>();
            CreateMap<CreateTypeDTO, TypeEntity>();
            CreateMap<UpdateTypeDTO, TypeEntity>();
        }
    }
}