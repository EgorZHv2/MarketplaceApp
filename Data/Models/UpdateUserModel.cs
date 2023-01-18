using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace Data.Models
{
    public class UpdateUserModel
    {
       
        public string? FirstName { get; set; }

        public  IFormFile? Photo { get; set; }
        
    }
}