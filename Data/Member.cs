namespace BisleriumCafe.Data
{
    // Represents a member
    public class Member
    {
        // Represents the member ID
        public long MemberID { get; set; }

        // Represents the updated number of orders for the member
        public int UpdatedNumberOfOrders { get; set; }

        // Represents the discount eligibility
        public bool MonthlyDiscount { get; set; } = false;
    }
}