using Backend.sso;

namespace Backend.BusinessLogic
{
    public class MyService
    {
        private SingleSignOnRegistry _registry;

        public MyService(SingleSignOnRegistry registry)
        {
            this._registry = registry;
        }

        public Response HandleRequest(Request request)
        {
            if (!_registry.IsValid(request.getSSOToken()))
            {
                return new Response("Please sign in before calling this service");
            }
            return new Response("hello " + request.getName() + "!");
        }
    }
}
