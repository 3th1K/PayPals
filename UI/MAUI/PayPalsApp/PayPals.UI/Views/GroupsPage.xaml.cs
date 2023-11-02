using System.Collections.ObjectModel;
using PayPals.UI.DTOs.GroupDTOs;
using PayPals.UI.Interfaces;
using PayPals.UI.Utilities;
using PayPals.UI.ViewModels;

namespace PayPals.UI.Views;

public partial class GroupsPage : ContentPage
{
	private readonly IStorageService _storageService;
    private readonly IUserService _userService;
    
    public UserGroupsViewModel _userGroups;

    public GroupsPage(IStorageService storageService, IUserService userService)
    {
        _storageService = storageService;
        _userService = userService;
        InitializeComponent();
        InitializeUserGroups();
        _userGroups = new UserGroupsViewModel();
        BindingContext = _userGroups;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var userGroups = await _storageService.GetUserGroupsAsync();
        if (userGroups == null)
        {
            await FetchUserGroups();
        }
    }
    private async void InitializeUserGroups()
    {
        var userGroups = await _storageService.GetUserGroupsAsync();
        if (userGroups == null)
        {
            await FetchUserGroups();
        }
        else
        {
            _userGroups.UserGroups = new ObservableCollection<GroupResponse>(userGroups);
        }
    }

    private async Task FetchUserGroups()
    {
        var user = await _storageService.GetUserAsync();
        if (user == null)
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
            return;
        }

        try
        {
            var userGroups = await _userService.GetUserGroupsAsync(user.UserId);
            if (userGroups.ResultStatus == ApiResultStatus.Success)
            {
                await _storageService.SetUserGroupsAsync(userGroups.SuccessResult);
                _userGroups.UserGroups = new ObservableCollection<GroupResponse>(await _storageService.GetUserGroupsAsync());
            }
            else
            {
                // do something
            }
        }
        catch (Exception ex)
        {
            //do something
        }
    }


    private async void Button_OnClicked(object sender, EventArgs e)
    {
        await _storageService.RemoveTokenAsync();
    }

    private async Task TapEffect(Frame frame)
    {
        var color = frame.BackgroundColor;
        frame.BackgroundColor = Colors.DarkGrey;
        await Task.Delay(100);
        frame.BackgroundColor = color;
    }
    private async void OnFrameTapped(object sender, TappedEventArgs e)
    {
        var frame = (Frame)sender;
        await TapEffect(frame);
        var tapGestureRecognizer = (TapGestureRecognizer)frame.GestureRecognizers[0];
        if (tapGestureRecognizer.CommandParameter != null)
        {
            int groupId = (int)tapGestureRecognizer.CommandParameter;

            await DisplayAlert("Success", $"{groupId}", "ok");
        }
    }
}
