using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Application.Comon;
using ShopOnline.Data.Data_Context;
using ShopOnline.Data.Entities;
using ShopOnline.Utill;
using ShopOnlineViewModel.Catalog.Product;
using ShopOnlineViewModel.Catalog.Product.Manage;
using ShopOnlineViewModel.Catalog.ProductImage;
using ShopOnlineViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Command.Products
{
    public class ManagementProductService : IProductManagementService
    {
        private readonly ShopOnline_Context _context;
        private readonly IStorageService _storageService;
        public ManagementProductService(ShopOnline_Context context, IStorageService  storageService)
        {
            _storageService = storageService;
            _context = context;
        }

      private async Task <string> SaveFile (IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
            var fileName= $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);

            return fileName;
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
            if (request.ThumbnailImange != null)
            {
                product.ProductImanges = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption= request.Name,
                        DateCreate = DateTime.Now,
                        FileSize = request.ThumbnailImange.Length,
                        ImagePath= await this.SaveFile(request.ThumbnailImange),
                        IsDefaul=true,
                        SortOder=1,
                    }
                };
            }
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();

        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id);
            if ( productTranslation == null && product == null)
            {
                throw new ShopOnlineException("can not find product");
            }
                        productTranslation.Name = request.Name;
                        productTranslation.Description = request.Description;
                        productTranslation.Details = request.Details;
                        productTranslation.SeoDescription = request.SeoDescription;
                        productTranslation.SeoAlias = request.SeoAlias;
                        productTranslation.SeoTitle = request.SeoTitle;
                        productTranslation.LanguageId = request.LanguageId;

            if (request.ThumbnailImange != null)
            {
                var ThumbnailImange = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefaul == true && i.ProdutId == request.Id);
               
                if (ThumbnailImange != null)
                {

                    ThumbnailImange.Caption = request.Name;
                    ThumbnailImange.DateCreate = DateTime.Now;
                    ThumbnailImange.FileSize = request.ThumbnailImange.Length;
                    ThumbnailImange.ImagePath = await this.SaveFile(request.ThumbnailImange);
                    ThumbnailImange.IsDefaul = true;
                    ThumbnailImange.SortOder = 1;
                    _context.ProductImages.Update(ThumbnailImange);
                }
            }

            return await _context.SaveChangesAsync();
        }
        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ShopOnlineException($"can not product {productId}");
            }

            var images =  _context.ProductImages.Where(i=> i.ProdutId == productId);
           foreach( var image in images)
            {
                _storageService.DeleteAsync(image.ImagePath);
            }    
           
            if (product == null)
            {
                throw new ShopOnlineException($"Can't find product{productId} ");
            }
        
            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }

    

        public async Task<PageResult<ProductViewModel>> GetProductPaging(GetProductPagingRequestManage request)
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

        public async Task<int> AddImange(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder
            };


         
         
        }

        public Task<int> UpdateImage(int ImageId, string caption, bool isDefault)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveImage(int imageId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductImageViewModel>> GetListImage(int ProductIsd)
        {
            throw new NotImplementedException();
        }
    }
}
