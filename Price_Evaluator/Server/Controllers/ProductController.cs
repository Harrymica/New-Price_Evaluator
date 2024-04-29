using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Price_Evaluator.Server.Services.ProductServices;

namespace Price_Evaluator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("getproducts/{search}")]   
        public async Task<ActionResult<List<Root>>> GetProduct(string search)
        {
            var result = await _service.GetProduct(search);
            
            return Ok(result);
        }
       

        [HttpGet("getcart")]
        public async Task<ActionResult<List<Cart>>> GetAllProductFromCart()
        {
            var result = await _service.GetCart();
            return Ok(result);
        }
        [HttpPost("cart")]
        public async Task<ActionResult<Cart>> AddToCart(Cart cart)
        {
            if (cart == null)
            {
                return BadRequest();
            }
            
              await _service.AddCart(cart);
            return Ok();
        }
    }
}
