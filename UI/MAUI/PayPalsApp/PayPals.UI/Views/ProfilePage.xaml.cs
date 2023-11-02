using PayPals.UI.Interfaces;
using PayPals.UI.Utilities;
using PayPals.UI.ViewModels;

namespace PayPals.UI.Views;

public partial class ProfilePage : ContentPage
{
    private readonly IStorageService _storageService;
    private readonly IRestService _restService;
    private readonly IUserService _userService;
    public UserViewModel userViewModel;
    public ProfilePage(IStorageService storageService, IRestService restService, IUserService userService)
    {
        _storageService = storageService;
        _restService = restService;
        _userService = userService;
        InitializeComponent();
        userViewModel = new UserViewModel();
        BindingContext = userViewModel;
        InitializeUser();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        userViewModel.User = await _storageService.GetUserAsync();
    }

    public async void InitializeUser()
    {
        var user = await _storageService.GetUserAsync();
        if (user == null)
        {
            var userId = await _storageService.ExtractUserIdFromToken();
            if (userId > 0)
            {
                var userResult = await _userService.GetUserDetailsAsync(userId);
                if (userResult.ResultStatus == ApiResultStatus.Success)
                {
                    user = userResult.SuccessResult;
                }
                else
                {
                    await _storageService.RemoveTokenAsync();
                    await DisplayAlert("Alert", "Please Log In Again", "Login");
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                    return;
                }
            }
            else
            {
                await _storageService.RemoveTokenAsync();
                await DisplayAlert("Alert", "Please Log In Again", "Login");
                await Shell.Current.GoToAsync(nameof(LoginPage));
                return;
            }
        }
        else
        {
            userViewModel.User = user;
        }
    }

    private async void BtnLogout_OnClicked(object sender, EventArgs e)
    {
        await _storageService.RemoveAllDataAsync();
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}