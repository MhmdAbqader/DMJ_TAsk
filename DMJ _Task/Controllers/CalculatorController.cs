using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMJ__Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Add(int x , int y) 
        {
            return Ok(x+y);
        }
    }
}
