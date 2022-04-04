using Backend.Outgoing;

namespace Backend.BusinessLogic
{
    public class JokeLogic
    {
        private Joke adapter;
        public JokeLogic() : this(new DadJoke())
        {
        }

        public JokeLogic(Joke adapter)
        {
            this.adapter = adapter;
        }

        public string Fetch(string? token)
        {
            var joke = adapter.Fetch();
            var db = new Database();
            if (joke == null || db.HasJoke(joke).Result)
            {
                joke = db.GetGoodJoke().Result;
            }
            else
            {
                db.InsertJoke(joke).Wait();
            }
            if (token != null)
            {
                db.BindJokeToUser(token, joke).Wait();
            }
            return joke;
        }

        public void Upvote(string? token)
        {
            if (token == null)
            {
                return;
            }
            var db = new Database();
            db.Upvote(token).Wait();
        }

        public void Downvote(string? token)
        {
            if (token == null)
            {
                return;
            }
            var db = new Database();
            db.Downvote(token).Wait();
        }
    }
}