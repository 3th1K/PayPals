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
}