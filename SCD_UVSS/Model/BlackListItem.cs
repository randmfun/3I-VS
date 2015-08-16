using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCD_UVSS.Model
{
    public class BlackListItem
    {
        public string VehicleNumber { get; set; }
    }


    public class BlackListItems: List<BlackListItem>
    {
       
    }
}
