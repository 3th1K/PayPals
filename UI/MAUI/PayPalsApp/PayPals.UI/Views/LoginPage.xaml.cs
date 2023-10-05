using System.Diagnostics;
using Microsoft.Extensions.Logging;
using PayPals.UI.Data;
using PayPals.UI.DTOs;
using PayPals.UI.Interfaces;
using PayPals.UI.Utilities;

namespace PayPals.UI.Views;

public partial class LoginPage : ContentPage
{
    private readonly ILoginService _loginService;
    public LoginPage(ILoginService loginService)
    {
        InitializeComponent();
        _loginService = loginService;
    }

    private string _username { get; set; }
    private string _password { get; set; }
    private async void BtnLogin_OnClicked(object sender, EventArgs e)
    {
        var request = new LoginRequest();
        request.Username = Username.Text;
        request.Password = Password.Text;
        //await DisplayAlert("Login", $"username : {_username}\npassword : {_password}", "OK");
        var x = await _loginService.DoLogin(request);
        if (x.ResultStatus == ApiResultStatus.Success)
        {
            await DisplayAlert("Success","Login Success !!", "Ok");
        }
        else
        {
            await DisplayAlert("Failed", $"{x.ErrorResult.ErrorDescription}", "Ok");
        }
    }

    private async void BtnRegister_OnClicked(object sender, EventArgs e)
    {
        Username.Text = string.Empty;
        Password.Text = string.Empty;
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }

    private void Username_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        Debug.Write(Username.BackgroundColor.ToString());
    }
}