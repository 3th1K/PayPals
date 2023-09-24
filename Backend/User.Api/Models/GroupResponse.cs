namespace UserService.Api.Models
{
    public class GroupResponse
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; } = null!;

        public int CreatorId { get; set; }

        public int TotalExpenses { get; set; }

        public virtual UserResponse Creator { get; set; } = null!;
    }
}
