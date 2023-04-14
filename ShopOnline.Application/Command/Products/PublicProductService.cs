﻿using Microsoft.EntityFrameworkCore;
using ShopOnline.Data.Data_Context;
using ShopOnline.Data.Entities;
using ShopOnline.Utill;
using ShopOnlineViewModel.Catalog.Product;
using ShopOnlineViewModel.Catalog.Product.Public;
using ShopOnlineViewModel.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Command.Products
{
    public class PublicProductService : IProductService

    {
        private readonly ShopOnline_Context _context;

        public PublicProductService(ShopOnline_Context context)
        {
            _context = context;
        }


     public async    Task<PageResult<ProductViewModel>> GetAllByCategory(GetProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            //fillter
           
            if (request.CategoryId.HasValue && request.CategoryId>0)
            {
                query = query.Where(x => x.pic.CategoryId== request.CategoryId);

            }
            //paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(k => new ProductViewModel()
                {
                    Id = k.p.Id,
                    Name = k.pt.Name,
                    Description = k.pt.Description,
                    Details = k.pt.Details,
                    LanguageId = k.pt.LanguageId,
                    OriginalPrice = k.p.OriginalPrice,
                    Price = k.p.Price,
                    Stock = k.p.Stock,
                    DateCreated = k.p.DateCreated,
                    ViewCount = k.p.ViewCount,
                    SeoAlias = k.pt.SeoAlias,
                    SeoTitle = k.pt.SeoTitle,
                    SeoDescription = k.pt.SeoDescription,

                }).ToListAsync();

            //select and project
            var pagedResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data,
            };
            return pagedResult;
        }

        public async Task  <List<ProductViewModel>> GetAll()
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            var data = await query.Select(k => new ProductViewModel()
             {
                 Id = k.p.Id,
                 Name = k.pt.Name,
                 Description = k.pt.Description,
                 Details = k.pt.Details,
                 LanguageId = k.pt.LanguageId,
                 OriginalPrice = k.p.OriginalPrice,
                 Price = k.p.Price,
                 Stock = k.p.Stock,
                 DateCreated = k.p.DateCreated,
                 ViewCount = k.p.ViewCount,
                 SeoAlias = k.pt.SeoAlias,
                 SeoTitle = k.pt.SeoTitle,
                 SeoDescription = k.pt.SeoDescription,

             }).ToListAsync();

       
           return data;
        }
        public async Task<ProductViewModel> GetById(int Id)
        {

            var product = await _context.Products.FindAsync(Id);
            if (product == null)
            {
                throw new ShopOnlineException($"can not product {Id}");
            }
    

        }
    }
}
