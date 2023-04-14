using ShopOnlineViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnlineViewModel.Catalog.Product.Manage
{
    public class GetProductPagingRequestManage : PageRequestsBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
