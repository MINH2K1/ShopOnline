using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
    

        public string ImagePath { get; set; }
        public string Caption { get; set; }

        public bool IsDefaul {get; set;}

        public DateTime DateCreate { get; set; }

        public int SortOder { get; set; }

        public long FileSize { get; set; }

        public int ProdutId { get; set; }
        public Product Product { get; set; }
    }
}
