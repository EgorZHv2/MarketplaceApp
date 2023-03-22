using Microsoft.AspNetCore.Http;

namespace Data.DTO
{
    public class CreateFileDTO
    {
         public IFormFile File { get; set; }
        public Guid EntityId { get; set; }
    }
}
