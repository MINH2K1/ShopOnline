using ShopOnline.Application.Command.Products.Dtos;
using ShopOnline.Application.Command.Products.Dtos.Manage;
using ShopOnline.Application.Command.Products.Dtos.Public;
using ShopOnline.Application.Dtos;
using ShopOnline.Data.Data_Context;
using ShopOnline.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Command.Products
{
    public class ManagementProductService : IProductManagementService
    {
        private readonly ShopOnline_Context _context;

        public ManagementProductService(ShopOnline_Context context)
        {
            _context = context;
        }

        public Task AddViewCount(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price

            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();

        }

        public Task<int> Delete(int productId, decimal newPrice)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetAll(ProductCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PageResult<ProductViewModel>> GetAllPage(string keyword, int pageIndex, int pagesize)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(ProductCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePrice(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStock(int productId)
        {
            throw new NotImplementedException();
        }
    }

}
