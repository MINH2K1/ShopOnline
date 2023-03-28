using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopOnlineViewModel.Common;

namespace ShopOnlineViewModel.Catalog.Product.Public
{
    public class GetProductPagingRequest : PageRequestsBase
    {
        public int? CategoryId { get; set; }
    }
}
