namespace Data.DTO.Shop
{
    public class ShopDTO:BaseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string INN { get; set; }
        public bool Blocked { get; set; }
        public Guid SellerId { get; set; }
        public string? ImagePath { get; set; }
    }
}
