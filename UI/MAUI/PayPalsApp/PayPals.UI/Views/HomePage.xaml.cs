using Microsoft.Maui.Controls;
using PayPals.UI.Interfaces;

namespace PayPals.UI.Views;

public partial class HomePage : ContentPage
{
	private readonly IStorageService _storageService;
	public HomePage(IStorageService storageService)
    {
        _storageService = storageService;
        NavigationPage.SetHasBackButton(this, false);
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // This code will run every time the page is rendered or appears.
        var user = await _storageService.GetUserAsync();
        Title = $"Welcome {user.Username}";

        // Add any other specific methods you want to run here.
    }

    private async Task TapEffect(Frame frame)
    {
        var color = frame.BackgroundColor;
        frame.BackgroundColor = Colors.DarkGrey;
        await Task.Delay(100);
        frame.BackgroundColor = color;
    }

    private async void OnBoxTapped(object sender, TappedEventArgs e)
    {
        var frame = (Frame)sender;
        await TapEffect(frame);

        var tapGestureRecognizer = (TapGestureRecognizer)frame.GestureRecognizers[0];
        string boxName = (string)tapGestureRecognizer.CommandParameter;

        switch (boxName)
        {
            case "Groups":
                await Shell.Current.GoToAsync(nameof(GroupsPage));
                break;
            case "Expenses":
                await Shell.Current.GoToAsync(nameof(ExpensesPage));
                break;
            case "Pals":
                await Shell.Current.GoToAsync(nameof(PalsPage));
                break;
            case "Profile":
                await Shell.Current.GoToAsync(nameof(ProfilePage));
                break;
        }
    }
}