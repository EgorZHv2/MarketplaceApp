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
    
        public CategoryController(IBaseDictionaryService<Category,CategoryDTO> baseDictionaryService):base(baseDictionaryService) {}
    }
}
