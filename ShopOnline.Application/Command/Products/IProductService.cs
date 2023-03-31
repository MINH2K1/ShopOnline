
using ShopOnlineViewModel.Catalog.Product;
using ShopOnlineViewModel.Catalog.Product.Public;
using ShopOnlineViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Command.Products
{
    public interface IProductService
    {
       Task<PageResult<ProductViewModel> >GetAllByCategory(GetProductPagingRequest request);
        Task<List<ProductViewModel>> GetAll();

    }
}
