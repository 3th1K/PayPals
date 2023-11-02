using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PayPals.UI.DTOs.GroupDTOs;

namespace PayPals.UI.ViewModels
{
    public partial class UserGroupsViewModel : ObservableObject
    {
        [ObservableProperty] 
        private ObservableCollection<GroupResponse> userGroups = new();

        [ObservableProperty] private GroupResponse groupResponse;
    }
}
