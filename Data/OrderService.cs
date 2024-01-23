using System.Text.Json;

namespace BisleriumCafe.Data
{
    // Class for managing order details
    internal class OrderService
    {
        // Retrieves order details from file
        public static List<Order> GetOrderDetails()
        {
            string orderFilePath = Utils.GetOrderPath();

            if (!File.Exists(orderFilePath))
            {
                return new List<Order>();
            }

            var json = File.ReadAllText(orderFilePath);
            var orders = JsonSerializer.Deserialize<List<Order>>(json);
            return orders;
        }

        // Saves order details to file
        public static void SaveOrderDetails(List<Order> transactions)
        {
            string orderFilePath = Utils.GetOrderPath();

            var json = JsonSerializer.Serialize(transactions);
            File.WriteAllText(orderFilePath, json);
        }

        // Creates a new order with the given transaction data
        public static List<Order> CreateNewOrder(Order transactionData)
        {
            // Retrieve existing order details
            List<Order> orders = GetOrderDetails();

            // Check if the item already exists
            bool itemExists = orders.Any(x => x.OrderID == transactionData.OrderID);

            // If it is not Saturday, add the new order
            if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday)
            {
                orders.Add(
                    new Order()
                    {
                        OrderID = new Guid(),
                        Products = transactionData.Products,
                        MemberID = transactionData.MemberID,
                        DateOfSale = DateTime.Now,
                        TotalPrice = transactionData.TotalPrice,
                    }
                );
            }

            // Save updated order details
            SaveOrderDetails(orders);

            return orders;
        }

        // Retrieves user transactions by user ID
        public static List<UserOrders> GetOrderByUserId(long userId)
        {
            string orderFilePath = Utils.GetOrderPath();

            if (!File.Exists(orderFilePath))
            {
                return new List<UserOrders>();
            }

            var json = File.ReadAllText(orderFilePath);
            var orders = JsonSerializer.Deserialize<List<Order>>(json);

            // Filter the orders list to retrieve only the orders for the given userId and current month
            var userOrderData = orders.Where(x => x.MemberID == userId && x.DateOfSale.Month == DateTime.Now.Month);

            var dataCount = userOrderData.Count();
            var data = new List<UserOrders>();

            foreach (var transaction in userOrderData)
            {
                data.Add(new UserOrders()
                {
                    UserID = transaction.MemberID,
                    DateOfSales = transaction.DateOfSale,
                });
            }

            return data;
        }
    }
}