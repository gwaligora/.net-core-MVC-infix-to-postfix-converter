using Microsoft.AspNetCore.Mvc;

namespace Programowanie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {   
        [HttpGet]
        public string Getstring()
        {
            string test = "test";
            return  test;
        }

    }

}