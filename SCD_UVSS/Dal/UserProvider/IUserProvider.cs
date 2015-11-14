using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCD_UVSS.Model;

namespace SCD_UVSS.Dal.UserProvider
{
    public interface IUserProvider
    {
        List<UserInfo> GetUsersList();
        void SaveUsers();
        void AddUser(UserInfo userInfo);
        void UpdatePassword(string userName, string password);
        void UpdateUserInfo(UserInfo userInfo, string username, string password);
    }
}
