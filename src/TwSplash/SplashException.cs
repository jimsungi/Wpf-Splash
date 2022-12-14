using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tigerword.twsplash
{
    public class SplashException : Exception
    {
        public SplashException(string message) : base(message)
        {
        }
    }
}
