using Microsoft.EntityFrameworkCore;
using ShopOnline.Application.Command.Products.Dtos;
using ShopOnline.Application.Command.Products.Dtos.Manage;
using ShopOnline.Application.Command.Products.Dtos.Public;
using ShopOnline.Application.Dtos;
using ShopOnline.Data.Data_Context;
using ShopOnline.Data.Entities;
using ShopOnline.Enitities.Excepition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>
                {
                    new ProductTranslation() {
                    Name= request.Name,
                    Description= request.Description,
                    Details=request.Details,
                    SeoDescription=request.SeoDescription,
                    SeoTitle=request.SeoTitle,
                    SeoAlias=request.SeoAlias,
                    LanguageId=request.LanguageId

                    }
                }
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();

        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ShopOnlineExeption($"Can't find product{productId} ");
            }

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public Task<List<ProductViewModel>> GetAll(ProductCreateRequest request)
        {
            throw new NotImplementedException();
        }



        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            //selectjoin
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };

            //filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));
            }
            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }
            //pagiing
            int TotalRow = await query.CountAsync();

            var data = query.Skip((request.PageSize-1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    Datecrated = x.p.Datecrated,
                    Description = x.pt.Description,
                    Details = x.pt.Details
                });

            //result
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = TotalRow,
                Items = await data.ToListAnsyc();
        };
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
