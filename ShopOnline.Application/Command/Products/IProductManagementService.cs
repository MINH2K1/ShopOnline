using ShopOnline.Application.Command.Products.Dtos;
using ShopOnline.Application.Command.Products.Dtos.Manage;
using ShopOnline.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Command.Products
{
    public interface IProductManagementService
    {
       Task< int> Create(ProductCreateRequest request);


        Task<int> Update(ProductCreateRequest request);
        Task<int>  Delete(int productId, decimal newPrice);
        Task<bool> UpdatePrice(int productId);
        Task<bool> UpdateStock(int productId);

        Task AddViewCount(int productId);
        Task<List<ProductViewModel>> GetAll(ProductCreateRequest request);
       Task< PageResult<ProductViewModel>> GetAllPage(string keyword, int pageIndex, int pagesize);

    }
}
