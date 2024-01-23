using System.Text.Json;

namespace BisleriumCafe.Data
{
    // Static class for managing member data
    public static class MemberService
    {
        // Retrieves member data from file
        public static List<Member> GetMemberData()
        {
            string memberFilePath = Utils.GetMemberPath();

            if (!File.Exists(memberFilePath))
            {
                return new List<Member>();
            }

            var json = File.ReadAllText(memberFilePath);
            var members = JsonSerializer.Deserialize<List<Member>>(json);
            return members;
        }

        // Creates a new member with the given user ID
        public static List<Member> CreateNewMember(long userId)
        {
            // Retrieve existing member data
            List<Member> members = GetMemberData();

            // Check if user already exists
            bool userExists = members.Any(x => x.MemberID == userId);

            // If user does not exist then add as a new member
            if (!userExists)
            {
                members.Add(new Member()
                {
                    MemberID = userId,
                    UpdatedNumberOfOrders = 1
                });
            }

            // Save updated member data
            SaveMemberData(members);

            return members;
        }

        public static List<Member> TransactionCount(long userID)
        {
            List<Member> members = GetMemberData();
            Member member = members.FirstOrDefault(members => members.MemberID == userID);

            if (member == null)
            {
                CreateNewMember(userID);
            }
            else
            {
                member.UpdatedNumberOfOrders += 1;
            }
            SaveMemberData(members);
            return members;
        }

        public static List<Member> DiscountStatus(long userID)
        {
            List<Member> members = GetMemberData();
            Member member = members.FirstOrDefault(members => members.MemberID == userID);

            if (member != null)
            {
                int x = OrderService.GetOrderByUserId(userID).Count;
                if (x % 10 == 0)
                {
                    member.MonthlyDiscount = true;
                }
                else
                {
                    member.MonthlyDiscount = false;
                }
            }
            SaveMemberData(members);
            return members;
        }

        // Saves member data to file
        public static void SaveMemberData(List<Member> members)
        {
            string memberFilePath = Utils.GetMemberPath();

            var json = JsonSerializer.Serialize(members);
            File.WriteAllText(memberFilePath, json);
        }

        
    }
}