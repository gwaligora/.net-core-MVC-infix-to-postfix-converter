
using Microsoft.AspNetCore.Mvc;
using Programowanie;

namespace Programowanie.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TokensController : ControllerBase
    {   
        [HttpGet]
        public IActionResult GetTokens(string formula = "2^2")
        {
            
            
          RPN obj = new RPN(formula);
          if(obj.Valid()){
              var succes = new {
                status = "ok",
                tokens = obj.TokensToArray(),
                postfix = obj.getPostfix()
              
              };
              return Ok(succes);

          }
            var failure = new{
                status = "error",
                message = "invalid formula"
            };
          return BadRequest(failure);
        }

    }

}