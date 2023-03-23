using ShopOnline.Application.Command.Products.Dtos;
using ShopOnline.Application.Command.Products.Dtos.Public;
using ShopOnline.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Command.Products
{
    public interface IProductService
    {
        PageResult<ProductViewModel> GetAllByCategory(GetProductPagingRequest request);

    }
}
