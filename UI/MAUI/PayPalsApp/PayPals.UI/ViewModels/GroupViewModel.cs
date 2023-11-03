using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PayPals.UI.DTOs.GroupDTOs;
using PayPals.UI.Interfaces;
using PayPals.UI.Utilities;

namespace PayPals.UI.ViewModels
{
    [QueryProperty(nameof(GroupId), "GroupId")]
    public partial class GroupViewModel : ObservableObject
    {
        private readonly IGroupService _groupService;
        public GroupViewModel(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [ObservableProperty] public GroupResponse group = new();

        private int groupId;
        public int GroupId
        {
            get => groupId; 
            //set => SetProperty(ref groupId, value);
            set
            {
                if (groupId != value)
                {
                    SetProperty(ref groupId, value);
                    FetchGroup();
                }
            }
        }

        public async void FetchGroup()
        {
            try
            {
                var groupResponse = await _groupService.GetGroupDetailsAsync(groupId);
                if (groupResponse.ResultStatus == ApiResultStatus.Success)
                {
                    Group = groupResponse.SuccessResult;
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
        }
    }
}
