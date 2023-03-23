using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Command.Products.Dtos.Manage
{
    public class ProductUpdateRequest
    {
        public string Name { get; set; }
        public decimal OriginalPrice { get; set; }
        public int CeoAlias { get; set; }
    }
}
