

using Microsoft.AspNetCore.Http;
using ShopOnlineViewModel.Catalog.Product;
using ShopOnlineViewModel.Catalog.Product.Manage;
using ShopOnlineViewModel.Common;
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

        Task<int> Update(ProductUpdateRequest request);
        Task <int>  Delete(int productId);
        Task<bool> UpdatePrice(int productId, decimal newPice);
        Task<bool> UpdateStock(int productId, int addQuantity);

        Task AddViewCount(int productId);

        Task<int> AddImange(int productId, List<IFormFile> files);

        Task<int> UpdateImage(int ImageId, string caption, bool isDefault);
        Task<int> RemoveImage(int imageId);

        


    }
}
