using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.Command.Products;
using ShopOnlineViewModel.Catalog.Product.Manage;
using ShopOnlineViewModel.Catalog.Product.Public;
using ShopOnlineViewModel.Catalog.ProductImage;

namespace ShopOnline.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _publicservice;
        private readonly IProductManagementService _productManagementService;
      public ProductController(IProductService publicservice, IProductManagementService productManagementService)
        {
            _publicservice = publicservice;
            _productManagementService = productManagementService;
        }
        //product
        [HttpGet]
        public async  Task <IActionResult> Get()
        {
            try
            {
                var a = await _publicservice.GetAll();
                return Ok(a);
            }
            catch
            {
                return BadRequest();
            }
         
        }
        //product/public-paging
        [HttpGet("public-paging")]
        public async Task<IActionResult> GetProductByCategory([FromQuery]GetProductPagingRequest request)
        {
            try
            {
                var result = await _publicservice.GetAllByCategory(request);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductById([FromQuery] int Id)
        {
            try
            {
                var result = await _publicservice.GetById(Id);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest request)
        {
            try
            {
                var productId = await _productManagementService.Create(request);
                
                if (productId == 0)
                {
                    return BadRequest();
                }
                var product = await _publicservice.GetById(productId);
                return CreatedAtAction(nameof(GetProductById), new { Id = productId }, product);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest request)
        {
            try
            {
                var ApffectResult = await _productManagementService.Update(request);

                if (ApffectResult == 0)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var ApffectResult = await _productManagementService.Delete(Id);

                if (ApffectResult == 0)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
     
    }
}
