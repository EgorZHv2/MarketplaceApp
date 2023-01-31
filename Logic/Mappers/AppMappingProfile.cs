using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Models;
using Data.DTO;
using Data.DTO.Shop;
using Data.DTO.Review;
using Data.DTO.User;
using Data.DTO.Auth;

namespace WebAPi.Mappers
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<RegistrationDTO, User>();

            CreateMap<Shop, ShopDTO>();
            CreateMap<CreateShopDTO, Shop>();
            CreateMap<UpdateShopDTO, Shop>();

            CreateMap<Review,ReviewDTO>();
            CreateMap<CreateReviewDTO,Review>();
            CreateMap<UpdateReviewDTO,Review>();

            CreateMap<User, UserDTO>();
            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();

            CreateMap<ChangePasswordDTO, User>().ReverseMap();
            CreateMap<CreateAdminModel, User>().ReverseMap();
           
            CreateMap<Shop, ShopDTO>().ForMember(x => x.ImagePath, opt => opt.Ignore());
           
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Data.Entities.Type, TypeDTO>().ReverseMap();
            CreateMap<DeliveryType, CreateDeliveryTypeDTO>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodDTO>().ReverseMap();
            
        }

       
    }
}
