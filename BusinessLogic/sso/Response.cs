namespace Backend.sso
{
    public class Response
    {
        private String text;

        public Response(String text)
        {
            this.text = text;
        }

        public String GetText()
        {
            return this.text;
        }
    }
}
