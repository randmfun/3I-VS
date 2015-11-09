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
        public UserInfo()
        {
            this.Name = string.Empty;
            this.Password = string.Empty;

            this.Role = Roles.Operator;
        }

        public string Name { get; set; }
        
        public string Password { get; set; }

        public Roles Role { get; set; }
    }
}
