
using Microsoft.AspNetCore.Mvc;
using Programowanie;

namespace Programowanie.Controllers
{
[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]

public class CalculateController: ControllerBase
{
    [HttpGet]
    public IActionResult resoultForX(string formula = "2^x", double x=0)
    {
        RPN obj = new RPN(formula, x);
        
        if(obj.Valid()){
            obj.getPostfix();
            var succes = new {
                status = "ok",
                result = obj.returnValue()
              
              };
            return Ok(succes);
        }
        var failure = new{
            status = "error",
            message = "invalid formula"

        };
        return BadRequest(failure);
        
    }
    
[HttpGet]
[Route("xy")]
public IActionResult resoultForXY(string formula = "2^x", double from = 0, double to = 10, int n =11)
    {
        RPN obj = new RPN(formula, from, to, n);
        if(obj.Valid()){
        obj.getPostfix();
            var succes = new {
                status = "ok",
                result = obj.returnValues()
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
