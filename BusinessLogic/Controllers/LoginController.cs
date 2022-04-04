using Backend.BusinessLogic;
using Backend.DataType;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        public LoginController()
        {
        }

        [HttpPost(Name = "PostLogin")]
        public string? Post([FromBody] NewUser user)
        {
            var logic = new UserLogic();
            return logic.Login(user.Uname, user.Pword);
        }
    }
}