using AutoMapper;
using Data.IRepositories;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Logic.Exceptions;
using System.Web;
using System.Net;
using System.Security.Claims;
using Logic.Interfaces;
using Data.DTO;
using Data.Entities;
using Data;
using Data.DTO.User;
using Microsoft.EntityFrameworkCore;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseGenericController<User,UserDTO,CreateUserDTO,UpdateUserDTO,IUserRepository,IUserService>
    {
        public UserController(IUserService userService) : base(userService) { }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Update(UserId, model);
            return Ok(result);
        }
    }
}
