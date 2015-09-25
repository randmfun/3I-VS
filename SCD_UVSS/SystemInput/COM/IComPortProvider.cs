using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCD_UVSS.SystemInput.COM
{
    public interface IComPortProvider
    {
        string Read();
        void Write(string message);
    }
}
