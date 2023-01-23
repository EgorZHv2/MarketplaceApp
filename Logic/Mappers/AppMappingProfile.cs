using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Models;
using Data.DTO;


namespace WebAPi.Mappers
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<RegistrationModel, User>();
            CreateMap<ReviewModel, Review>().ReverseMap();
            CreateMap<ShopModel, Shop>().ReverseMap();
            CreateMap<ChangePasswordModel, User>().ReverseMap();
            CreateMap<CreateAdminModel, User>().ReverseMap();
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<Shop,ShopDTO>().ForMember(x => x.ImagePath, opt => opt.Ignore());
            CreateMap<Review,ReviewDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Data.Entities.Type, TypeDTO>().ReverseMap();
            CreateMap<DeliveryType, DeliveryTypeDTO>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodDTO>().ReverseMap();

            
        }
    }
}
