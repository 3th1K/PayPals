using CommunityToolkit.Mvvm.ComponentModel;
using PayPals.UI.DTOs.UserDTOs;

namespace PayPals.UI.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        [ObservableProperty] private UserDetailsResponse user = new();
    }
}
