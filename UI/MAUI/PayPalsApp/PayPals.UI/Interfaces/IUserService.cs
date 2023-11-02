using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayPals.UI.DTOs.GroupDTOs;
using PayPals.UI.DTOs.UserDTOs;
using PayPals.UI.Utilities;

namespace PayPals.UI.Interfaces
{
    public interface IUserService
    {
        Task<ApiResult<UserDetailsResponse>> GetUserDetailsAsync(int id);
        Task<ApiResult<List<GroupResponse>>> GetUserGroupsAsync(int id);
    }
}
