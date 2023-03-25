﻿
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
        Task<int>  Delete(int productId);
        Task<bool> UpdatePrice(int productId, decimal newPice);
        Task<bool> UpdateStock(int productId, int addQuantity);

        Task AddViewCount(int productId);

       Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);

    }
}
