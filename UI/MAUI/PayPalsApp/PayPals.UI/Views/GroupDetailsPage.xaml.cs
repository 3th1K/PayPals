using Mopups.Services;
using PayPals.UI.ViewModels;

namespace PayPals.UI.Views;

public partial class GroupDetailsPage : ContentPage
{
	public GroupViewModel GroupViewModel;
	public GroupDetailsPage(GroupViewModel groupViewModel)
    { 
        GroupViewModel = groupViewModel;
        InitializeComponent();
        BindingContext = GroupViewModel;
    }

    //protected override bool OnBackButtonPressed()
    //{
    //    base.OnBackButtonPressed();
    //    Shell.Current.Navigation.PopAsync();
    //    return true;
    //}
}