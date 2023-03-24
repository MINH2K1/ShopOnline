using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Enitities.Excepition
{
    public class ShopOnlineExeption:Exception
    {
        public ShopOnlineExeption() { }
        public ShopOnlineExeption(string message) : base(message)
        {

        }
        public ShopOnlineExeption(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
