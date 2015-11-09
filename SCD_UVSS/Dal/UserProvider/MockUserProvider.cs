using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCD_UVSS.Model;

namespace SCD_UVSS.Dal.UserProvider
{
    public class MockUserProvider: IUserProvider
    {
        private List<UserInfo> savedUserInfos;

        public MockUserProvider(List<UserInfo> savedUserInfos)
        {
            this.savedUserInfos = savedUserInfos;
        }

        public List<UserInfo> GetUsersList()
        {
            return this.savedUserInfos;
        }

        public void SaveUsers()
        {
            
        }

        public void AddUser(UserInfo userInfo)
        {
            this.savedUserInfos.Add(userInfo);
        }
    }
}
