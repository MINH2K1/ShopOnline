using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopOnline.Application.Dtos;

namespace ShopOnline.Application.Command.Products.Dtos.Public
{
    public class GetProductPagingRequest : PageRequestsBase
    {
        public int CategoryId { get; set; }
    }
}
