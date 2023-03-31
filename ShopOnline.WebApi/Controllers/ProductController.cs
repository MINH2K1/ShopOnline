using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.Command.Products;

namespace ShopOnline.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _publicservice;
      public ProductController(IProductService publicservice)
        {
            _publicservice = publicservice;
        }
        [HttpGet]
        public async  Task <IActionResult> Get()
        {
            var a = await  _publicservice.GetAll();
            return Ok(a);
        }

    }
}
