using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCD_UVSS.Utils
{
    public static class Logging
    {
        public static void Errors(string message, bool throwException=false)
        {

            if (throwException)
            {
                throw new Exception(message);
            }
        }

        public static void Warning(string message)
        {
            
        }
    }
}
