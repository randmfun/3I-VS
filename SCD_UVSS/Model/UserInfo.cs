using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCD_UVSS.Model
{
    public enum Roles
    {
        Developer,
        Admin,
        Operator
    }

    [Serializable]
    public class UserInfo
    {
        private string _id;

        public UserInfo()
        {
            this.Name = string.Empty;
            this.Password = string.Empty;
            this._id = Guid.NewGuid().ToString();
            this.Role = Roles.Operator;
        }

        public string ID
        {
            get { return this._id; }
        }

        public string Name { get; set; }
        
        public string Password { get; set; }

        public Roles Role { get; set; }
    }
}
