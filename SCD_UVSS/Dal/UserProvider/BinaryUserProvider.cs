using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SCD_UVSS.helpers;
using SCD_UVSS.Model;

namespace SCD_UVSS.Dal.UserProvider
{
    public class BinaryUserProvider : IUserProvider
    {
        private const string SAVE_FILE_NAME = "app.users";

        private List<UserInfo> _savedUserInfos;

        public List<UserInfo> GetUsersList()
        {
            return this._savedUserInfos ?? (this._savedUserInfos = this.ReadSavedUserInfos());
        }

        public void AddUser(UserInfo userInfo)
        {
            this.GetUsersList().Add(userInfo);
        }
        
        public void SaveUsers()
        {
            SerializeUtility.Save(this.GetUsersList(), SAVE_FILE_NAME);
        }

        public List<UserInfo> ReadSavedUserInfos()
        {
            if(File.Exists(SAVE_FILE_NAME))
                return SerializeUtility.Read<List<UserInfo>>(SAVE_FILE_NAME);
                
            return new List<UserInfo>();
        }

        /// <summary>
        /// TESTING HACK ONLY
        /// </summary>
        /// <param name="userInfos"></param>
        public void SetSavedUserInfos(List<UserInfo> userInfos)
        {
            this._savedUserInfos = userInfos;
        }

    }
}
