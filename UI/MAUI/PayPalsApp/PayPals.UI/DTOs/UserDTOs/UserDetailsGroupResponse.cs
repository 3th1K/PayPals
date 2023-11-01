namespace PayPals.UI.DTOs.UserDTOs;

public class UserDetailsGroupResponse
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public int CreatorId { get; set; }

    public int TotalExpenses { get; set; }
}