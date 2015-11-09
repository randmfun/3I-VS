using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor.Installer;
using SCD_UVSS.Dal.UserProvider;
using SCD_UVSS.Model;

namespace SCD_UVSS.Dal
{
    public class UserManager
    {

        private UserInfo _loggedInUser;

        public readonly IUserProvider UserProvider;

        public UserManager(IUserProvider provider)
        {
            this.UserProvider = provider;
        }
        
        public UserInfo LoggedInUser
        {
            get { return this._loggedInUser; }
        }

        public List<UserInfo> GetUsersList()
        {
            return this.UserProvider.GetUsersList();
        }

        public void AddUsers(UserInfo userInfo)
        {
            this.UserProvider.AddUser(userInfo);
        }

        public void SaveUsers()
        {
            this.UserProvider.SaveUsers();
        }

        public void SetLoggedInUser(UserInfo userInfo)
        {
            this._loggedInUser = userInfo;
        }

        public void SetLoggedInUser(string userName)
        {
            var users = this.GetUsersList();

            var info = from userInfo in users
                       where userInfo.Name == userName
                       select userInfo;

            this._loggedInUser = info.FirstOrDefault();
        }

        public bool IsUserValid(string userName, string password, out string errorMsg)
        {
            errorMsg = string.Empty;
            var users = this.GetUsersList();

            var isValidUser = users.Any(user => user.Name == userName);
            if (!isValidUser)
            {
                errorMsg = "No such user in the system : " + userName;
                return false;
            }

            var isValidPassword = users.Any(user => (user.Name == userName && user.Password == password));
            if (!isValidPassword)
            {
                errorMsg = "Entered Password is In Valid !!";
                return false;
            }

            return true;
        }
    }
}
