using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.Command.Products;
using ShopOnlineViewModel.Catalog.Product.Manage;
using ShopOnlineViewModel.Catalog.Product.Public;

namespace ShopOnline.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _publicservice;
        private readonly IProductManagementService _productManagementService;
      public ProductController(IProductService publicservice,
          IProductManagementService productManagementService)
        {
            _publicservice = publicservice;
            _productManagementService=productManagementService;
        }
        [HttpGet]
        public async  Task <IActionResult> Get()
        {
            var a = await  _publicservice.GetAll();
            return Ok(a);
        }
        [HttpGet("get-paging")]
        public async Task<IActionResult> GetProductPagingPage([FromQuery] GetProductPagingRequestManage request)
        {
            var Result = _productManagementService.GetProductPaging(request);
            if(Result == null)
            {
                return BadRequest("Don't have ");
            }
            return Ok(Result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ProductCreateRequest request)
        {
            var result = _productManagementService.Create(request);
            return Ok(result);
        }
    }
}
