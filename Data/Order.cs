namespace BisleriumCafe.Data
{
    // Represents an order
    public class Order
    {
        // Represents the order ID, generated using Guid.NewGuid()
        public Guid OrderID { get; set; } = Guid.NewGuid();

        // Represents the list of products in the order
        public List<Product> Products { get; set; }

        // Represents the member ID associated with the order
        public long MemberID { get; set; }

        // Represents the date of sale for the order
        public DateTime DateOfSale { get; set; }

        // Represents the total price of the order
        public float TotalPrice { get; set; }
    }
}