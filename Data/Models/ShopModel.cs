using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class ShopModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string INN { get; set; }

        public bool Blocked { get; set; } = false;

        [Required]
        public Guid SellerId { get; set; }

        public List<Guid> CategoriesId { get; set; } = new List<Guid>();
        public List<Guid> TypesId { get; set; }= new List<Guid>();
        public List<Guid> DeliveryTypesId { get; set; }= new List<Guid>();
        public List<Guid> PaymentMethodsId { get; set; }= new List<Guid>();
    }
}