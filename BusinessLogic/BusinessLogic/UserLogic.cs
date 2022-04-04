using Backend.Outgoing;

namespace Backend.BusinessLogic
{
    public class UserLogic
    {
        public UserLogic()
        {
        }

        public string? Login(string uname, string pword)
        {
            var db = new Database();
            return db.Login(uname, pword).Result;
        }

        public string Create(string uname, string pword)
        {
            var db = new Database();
            return db.CreateUser(uname, pword).Result;
        }
    }
}