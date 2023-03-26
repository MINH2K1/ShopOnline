using ShopOnline.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Command.Products.Dtos.Manage
{
    public class GetProductPagingRequest : PageRequestsBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
