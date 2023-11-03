using PayPals.UI.Interfaces;

namespace PayPals.UI.Views;

public partial class LoadingPage : ContentPage
{
	private readonly IStorageService _storageService;
	public LoadingPage(IStorageService storageService)
    {
        _storageService = storageService;
        InitializeComponent();
    }
    private async Task<bool> IsLoggedIn()
    {
        var token = await _storageService.GetTokenAsync();
        if (token == null)
        {
            return false;
        }
        return true;
    }
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (await IsLoggedIn())
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
        else
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}