using System.Text.Json;
using System.Text.Json.Serialization;
using PayPals.UI.DTOs;
using PayPals.UI.Interfaces;
using PayPals.UI.Utilities;

namespace PayPals.UI.Views;

public partial class LoginPage : ContentPage
{
    private readonly ILoginService _loginService;
    private readonly IStorageService _storageService;
    private readonly IUserService _userService;
    public LoginPage(ILoginService loginService, IStorageService storageService, IUserService userService)
    {
        InitializeComponent();
        _loginService = loginService;
        _storageService = storageService;
        _userService = userService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (await IsLoggedIn())
        {
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
    }

    private async Task<bool> IsLoggedIn()
    {
        try
        {
            _ = await _storageService.GetTokenAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    private async void HandleLoginErrors(Error err) 
    {
        switch (err.ErrorCode) 
        {
            case 10002: 
                var doRegister = await DisplayAlert("Warning", $"User {Username.Text} is not registered", "Register", "Ok");
                Username.Text = string.Empty;
                Password.Text = string.Empty;
                if (doRegister) 
                {
                    await Shell.Current.GoToAsync(nameof(RegisterPage));
                }
                break;
            case 10001:
                await DisplayAlert("Unauthorized", $"Please Provide Correct Credentials", "Ok");
                Username.Text = string.Empty;
                Password.Text = string.Empty;
                break;
            case 101: 
                await DisplayAlert("Failed", $"bad bad", "Ok");
                break;
            default:
                await DisplayAlert("Failed", $"unknown", "Ok");
                break;
        }
    }
    
    private async void BtnLogin_OnClicked(object sender, EventArgs e)
    {
        var request = new LoginRequest
        {
            Username = Username.Text,
            Password = Password.Text
        };

        var response = await _loginService.DoLogin(request);
        if (response.ResultStatus == ApiResultStatus.Success)
        {
            //await DisplayAlert("Success",$"{response.SuccessResult}", "Ok");
            var token = response.SuccessResult;
            try
            {
                await _storageService.SetTokenAsync(token);
                int userId = await _storageService.ExtractUserIdFromToken();
                var userDetails = await _userService.GetUserDetailsAsync(userId);
                if (userDetails.ResultStatus == ApiResultStatus.Success)
                {
                    //await DisplayAlert("Success", $"User : {JsonSerializer.Serialize(userDetails.SuccessResult)}","Ok");
                    await _storageService.SetUserAsync(userDetails.SuccessResult);
                    await Shell.Current.GoToAsync(nameof(HomePage));
                }
                else
                {
                    await DisplayAlert("Failure", $"Error : {userDetails.ErrorResult}", "Ok");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("error", $"error : {ex.Message}", "error");
            }



        }
        else
        {
            HandleLoginErrors(response.ErrorResult);
        }
    }

    private async void BtnRegister_OnClicked(object sender, EventArgs e)
    {
        Username.Text = string.Empty;
        Password.Text = string.Empty;
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }

    private void HandleLoginButtonVisibility() 
    {
        if (Username.Text == null || Password.Text == null) 
        {
            BtnLogin.IsEnabled = false;
            BtnLogin.BackgroundColor = Colors.LightSlateGrey;
            return;
        }
        if (Username.Text.Length >= 5 && Password.Text.Length != 0)
        {
            BtnLogin.IsEnabled = true;
            BtnLogin.BackgroundColor = Colors.LightSkyBlue;
        }
        else
        {
            BtnLogin.IsEnabled = false;
            BtnLogin.BackgroundColor = Colors.LightSlateGrey;
        }
    }

    private void Username_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        HandleLoginButtonVisibility();
    }

    private void Password_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        HandleLoginButtonVisibility();
    }
}