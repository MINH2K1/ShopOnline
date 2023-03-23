using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Dtos
{
    public class PageRequestsBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
