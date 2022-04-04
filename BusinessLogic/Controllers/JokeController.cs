using Backend.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JokeController : ControllerBase
    {
        public JokeController()
        {
        }

        [HttpPost(Name = "GetJoke")]
        public DataType.Joke Get([FromBody] string? token)
        {
            var logic = new JokeLogic();
            return new DataType.Joke
            {
                Text = logic.Fetch(token)
            };
        }

        [HttpPost("upvote", Name = "Upvote")]
        public void Upvote([FromBody] string? token)
        {
            var logic = new JokeLogic();
            logic.Upvote(token);
        }

        [HttpPost("downvote", Name = "Downvote")]
        public void Downvote([FromBody] string? token)
        {
            var logic = new JokeLogic();
            logic.Downvote(token);
        }
    }
}