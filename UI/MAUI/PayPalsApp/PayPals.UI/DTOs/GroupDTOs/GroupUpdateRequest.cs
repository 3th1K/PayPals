namespace PayPals.UI.DTOs.GroupDTOs
{
    public class GroupUpdateRequest
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public int CreatorId { get; set; }
        public int TotalExpenses { get; set; }
    }
}
