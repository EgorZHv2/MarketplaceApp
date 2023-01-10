using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Logic.Models;
using WebAPi.Models;

namespace WebAPi.Mappers
{
    public class AppMappingProfile:Profile
    {
        public AppMappingProfile() 
        {
            CreateMap<RegistrationModel, User>();

            CreateMap<ReviewModel,Review>().ReverseMap();

            CreateMap<ShopModel,Shop>().ReverseMap();

            CreateMap<UpdateUserModel, User>().ReverseMap();
        }
    }
}
