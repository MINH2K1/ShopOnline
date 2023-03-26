using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Utill
{
    public class ShopOnlineException:Exception

    {
        public ShopOnlineException() { }
        public static ShopOnlineException(string message) : base(message)
        {

        }
        public ShopOnlineException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
