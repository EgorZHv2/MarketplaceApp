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
            CreateMap<UpdateUserModel, User>().ReverseMap();
            CreateMap<CreateAdminModel, User>().ReverseMap();
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<Shop,ShopDTO>().ReverseMap();
            CreateMap<Review,ReviewDTO>().ReverseMap();
            CreateMap<UsersFavShops, UsersFavShopsDTO>().ReverseMap();
            
        }
    }
}
