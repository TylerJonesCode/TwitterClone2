using Microsoft.AspNetCore.Mvc;
using testapi.Data;

namespace testapi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]

    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }
    
    [HttpGet("GetUsers")]
    public string[] GetUsers()
    {  
       return new string[] {"im sad", "you are sad"}; 
    }
  
    
    [HttpGet()]
    public IActionResult Login()
    {
        return Ok();
    }
    
}
