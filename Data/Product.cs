namespace BisleriumCafe.Data
{
    // Represents a product
    public class Product
    {
        // Represents the required product name
        public required string ProductName { get; set; }

        // Represents the product price
        public float ProductPrice { get; set; }

        // Represents the product type
        public ProductType ProductType { get; set; }

        // Represents the quantity
        public int Quantity { get; set; }
    }
}