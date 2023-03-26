using Microsoft.EntityFrameworkCore;
using ShopOnline.Application.Command.Products.Dtos;
using ShopOnline.Application.Command.Products.Dtos.Manage;
using ShopOnline.Application.Dtos;
using ShopOnline.Data.Data_Context;
using ShopOnline.Data.Entities;
using ShopOnline.Utill;
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

      

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = request.ViewCount,
                DateCreated = DateTime.Now,
                ProductTranslations= new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name=request.Name,
                        Description=request.Description,
                        Details= request.Details,
                        SeoDescription= request.SeoDescription,
                        SeoAlias= request.SeoAlias,
                        SeoTitle= request.SeoTitle,
                        LanguageId= request.LanguageId,
                    }
                }
                
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();

        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null)
                throw new ShopOnlineException("can not find product");
            
            _context.Products.Update(product);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ShopOnlineException($"Can't find product{productId} ");
            }

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

    

        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequestManage request)
       {
            //select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            //fillter
            if (!string.IsNullOrEmpty(request.Keyword)){
              query=  query.Where(x => x.pt.Name.Contains(request.Keyword));
            }
            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(x => request.CategoryIds.Contains(x.pic.CategoryId));

            }
            //paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(k => new ProductViewModel()
                {
                    Id = k.p.Id,
                    Name= k.pt.Name,
                    Description= k.pt.Description,
                    Details= k.pt.Details,
                    LanguageId= k.pt.LanguageId,
                    OriginalPrice= k.p.OriginalPrice,
                    Price= k.p.Price,
                    Stock=k.p.Stock,
                    DateCreated= k.p.DateCreated,
                    ViewCount= k.p.ViewCount,
                    SeoAlias= k.pt.SeoAlias,
                    SeoTitle= k.pt.SeoTitle,
                    SeoDescription= k.pt.SeoDescription,

                }).ToListAsync();

            //select and project
            var pagedResult = new PageResult<ProductViewModel >()
            {
                TotalRecord = totalRow,
                Items = data,
            };
            return pagedResult;
        }


      
        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ShopOnlineException($"can not find {productId}");
            }
            product.Price=newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ShopOnlineException($"can not find {productId}");
            }
            product.Stock += addQuantity;
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
