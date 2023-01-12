namespace Data.DTO
{
    public class ShopDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Logo { get; set; }
        public string INN { get; set; }
        public bool Blocked { get; set; }
        public Guid SellerId { get; set; }
    }
}
