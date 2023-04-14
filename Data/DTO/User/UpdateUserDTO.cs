using Data.DTO.BaseDTOs;
using Microsoft.AspNetCore.Http;

namespace Data.DTO.User
{
    public class UpdateUserDTO : BaseUpdateDTO
    {
        public string? FirstName { get; set; }

        public IFormFile? Photo { get; set; }
    }
}