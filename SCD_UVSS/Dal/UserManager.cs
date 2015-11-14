using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor.Installer;
using log4net;
using SCD_UVSS.Dal.UserProvider;
using SCD_UVSS.Model;

namespace SCD_UVSS.Dal
{
    public class UserManager
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(UserManager));

        private UserInfo _loggedInUser;
        private static UserManager _instance = null;

        public readonly IUserProvider UserProvider;

        public static UserManager Instance
        {
            get
            {
                return _instance ?? (_instance = new UserManager(new BinaryUserProvider()));
            }
        }

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
            logger.Info("Adding User" + userInfo.Name);
            this.UserProvider.AddUser(userInfo);
            logger.Info("Done Adding User" + userInfo.Name);
        }

        public void SaveUsers()
        {
            logger.Info("Saving Users");
            this.UserProvider.SaveUsers();
            logger.Info("Saved Users");
        }

        public void UpdateUserInfo(UserInfo userInfo, string username, string password)
        {
            logger.Info("Updating User" + userInfo.Name);
            this.UserProvider.UpdateUserInfo(userInfo, username, password);
            logger.Info("Updated User" + userInfo.Name);
        }

        public void UpdatePassword(string username, string password)
        {
            logger.Info("Updating Password" + username);
            this.UserProvider.UpdatePassword(username, password);
            logger.Info("Updated Password" + username);
        }

        public void SetLoggedInUser(UserInfo userInfo)
        {
            logger.Info("Logged in user" + userInfo.Name);
            this._loggedInUser = userInfo;
        }

        public void SetLoggedInUser(string userName)
        {
            logger.Info("Logged in user" + userName);
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
                errorMsg = "Invalid User Name : " + userName;
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
