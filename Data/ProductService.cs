using System.Text.Json;

namespace BisleriumCafe.Data
{
    // Static class for managing product data
    public static class ProductService
    {
        // Saves product data to file
        public static void SaveProduct(List<Product> items)
        {
            string itemFilePath = Utils.GetProductsPath();

            var json = JsonSerializer.Serialize(items);
            File.WriteAllText(itemFilePath, json);
        }

        // Retrieves product data from file
        public static List<Product> GetProductInfo()
        {
            string itemFilePath = Utils.GetProductsPath();

            if (!File.Exists(itemFilePath))
            {
                return new List<Product>();
            }

            var json = File.ReadAllText(itemFilePath);
            var items = JsonSerializer.Deserialize<List<Product>>(json);
            return items;
        }

        // Creates a new product with the given name, price, and type
        public static List<Product> CreateNewProduct(string itemName, float itemPrice, ProductType itemType)
        {
            // Retrieve existing product information
            List<Product> items = GetProductInfo();

            // Check if the item already exists
            bool itemExists = items.Any(x => x.ProductName == itemName);

            if (itemExists)
            {
                throw new Exception($"{itemName} already exists!");
            }

            // Add the new product
            items.Add(
                new Product
                {
                    ProductName = itemName,
                    ProductPrice = itemPrice,
                    ProductType = itemType
                }
            );

            // Save updated product information
            SaveProduct(items);

            return items;
        }

        // Updates the price of an existing product
        public static List<Product> UpdateProductPrice(string itemName, float itemPrice)
        {
            // Retrieve existing product information
            List<Product> items = GetProductInfo();

            // Find the product to update
            Product item = items.FirstOrDefault(x => x.ProductName == itemName);

            if (item == null)
            {
                throw new Exception("This item could not be found!");
            }

            // Update the product price
            item.ProductPrice = itemPrice;

            // Save updated product information
            SaveProduct(items);

            return items;
        }

        // Deletes an existing product
        public static List<Product> DeleteExistingProduct(string itemName)
        {
            // Retrieve existing product information
            List<Product> items = GetProductInfo();

            // Find the product to delete
            Product item = items.FirstOrDefault(x => x.ProductName == itemName);

            if (item == null)
            {
                throw new Exception("This item could not be found!");
            }

            // Remove the product
            items.Remove(item);

            // Save updated product information
            SaveProduct(items);

            return items;
        }
    }
}