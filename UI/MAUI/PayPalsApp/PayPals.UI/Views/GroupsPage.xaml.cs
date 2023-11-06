using System.Collections.ObjectModel;
using Mopups.Services;
using PayPals.UI.DTOs.GroupDTOs;
using PayPals.UI.Interfaces;
using PayPals.UI.Utilities;
using PayPals.UI.ViewModels;

namespace PayPals.UI.Views;

public partial class GroupsPage : ContentPage
{
	private readonly IStorageService _storageService;
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;
    
    public UserGroupsViewModel _userGroups;
    private int CurrentUserId = -1;

    public GroupsPage(IStorageService storageService, IUserService userService, IGroupService groupService)
    {
        _storageService = storageService;
        _userService = userService;
        _groupService = groupService;
        InitializeComponent();
        InitializeUserGroups();
        _userGroups = new UserGroupsViewModel();
        BindingContext = _userGroups;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (CurrentUserId < 0)
        {
            var user = await _storageService.GetUserAsync();
            if (user == null)
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
                return;
            }
            CurrentUserId = user.UserId;
        }

        var userGroups = await _storageService.GetUserGroupsAsync();
        if (userGroups == null)
        {
            await FetchUserGroups();
            return;
        }

        if (userGroups.Count != _userGroups.UserGroups.Count)
        {
            _userGroups.UserGroups = new ObservableCollection<GroupResponse>(userGroups);
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
        await MopupService.Instance.PushAsync(new LoadingPopupPage("Loading Groups"));
        var user = await _storageService.GetUserAsync();
        if (user == null)
        {
            await MopupService.Instance.PopAsync();
            await Shell.Current.GoToAsync(nameof(LoginPage));
            return;
        }

        try
        {
            CurrentUserId = user.UserId;
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

        await MopupService.Instance.PopAsync();
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
        //await MopupService.Instance.PushAsync(new LoadingPopupPage("Loading"));
        var frame = (Frame)sender;
        await TapEffect(frame);
        frame.IsEnabled = false;
        var tapGestureRecognizer = (TapGestureRecognizer)frame.GestureRecognizers[0];
        if (tapGestureRecognizer.CommandParameter != null)
        {
            int groupId = (int)tapGestureRecognizer.CommandParameter;
            //await MopupService.Instance.PopAsync();
            await Shell.Current.GoToAsync($"{nameof(GroupDetailsPage)}?GroupId={groupId}");
        }
        frame.IsEnabled = true;
    }

    private async void BtnAddGroup_OnClicked(object sender, EventArgs e)
    {
        var btn = (ImageButton)sender;
        var height = btn.Height;
        btn.HeightRequest = height-10;
        await Task.Delay(100);
        btn.HeightRequest = height;
        btn.IsEnabled = false;
        var x = await DisplayPromptAsync("Create Group", "Enter the Group Name", "Ok", "Cancel", null, 20, null,
            $"Group-{Guid.NewGuid().ToString().Substring(0, 5)}");
        var groupRequest = new GroupRequest
        {
            GroupName = x,
            CreatorId = CurrentUserId > 0? CurrentUserId : 0,
        };

        try
        {
            var createdGroup = await _groupService.CreateGroupAsync(groupRequest);
            if (createdGroup.ResultStatus == ApiResultStatus.Success)
            {
                var groupMember = new GroupMember
                {
                    UserId = this.CurrentUserId
                };
                var response = await _groupService.AddGroupMemberAsync(createdGroup.SuccessResult.GroupId, groupMember);
                if (response.ResultStatus == ApiResultStatus.Success)
                {
                    await _storageService.AddUserGroupAsync(response.SuccessResult);
                    _userGroups.UserGroups.Add(response.SuccessResult);
                }
            }
            else
            {
                // do something
            }
        }
        catch (Exception ex)
        {
            // do something
        }

        btn.IsEnabled = true;
    }

    private async void RefreshGroups_OnRefreshing(object sender, EventArgs e)
    {
        _userGroups.IsRefreshing = true;
        await FetchUserGroups();
        _userGroups.IsRefreshing = false;
    }
}
