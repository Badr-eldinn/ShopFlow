namespace ShopFlow.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

    }
    public static class ShopFlowDataStore
    {
        public static List<Product> Products { get; } = new()
        {
            new Product { Id = 1,Name= "bag",Price=250m,StockQuantity=100 },
            new Product { Id = 2,Name= "hand bag",Price=2250m,StockQuantity=100 },
            new Product { Id = 3,Name= "breclet",Price=250m,StockQuantity=100 }


        };
        public static Dictionary<string, List<CartItem>> Carts { get; } = new();


    }
}
