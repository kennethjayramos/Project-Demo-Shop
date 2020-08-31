namespace Demo_Shop.Core.ViewModels
{
    public class BasketItemViewModel
    {
        // Id of the basket item
        public string Id { get; set; }

        public int Quantity { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }
    }
}