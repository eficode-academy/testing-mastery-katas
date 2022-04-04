namespace Backend.sso
{
    public interface AuthenticationGateway
    {
        bool CredentialsAreValid(String username, String password);
    }
}
