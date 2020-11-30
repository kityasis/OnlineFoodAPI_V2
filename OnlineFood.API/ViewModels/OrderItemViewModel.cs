namespace OnlineFood.API.ViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductId { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductMRP { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
    }
}
