using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnlineViewModel.Catalog.Product
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }

        public bool isDefaul { get; set; }
        public long FileSize { get; set; }
    }
}
