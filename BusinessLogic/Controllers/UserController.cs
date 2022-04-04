using Backend.BusinessLogic;
using Backend.DataType;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }

        [HttpPost(Name = "PostUser")]
        public void Post([FromBody] NewUser user)
        {
            var logic = new UserLogic();
            logic.Create(user.Uname, user.Pword);
        }
    }
}