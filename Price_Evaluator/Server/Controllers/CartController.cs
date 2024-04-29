using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Price_Evaluator.Server.Database;

namespace Price_Evaluator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public CartController(DataContext context)
        {
                
        }
    }
}
