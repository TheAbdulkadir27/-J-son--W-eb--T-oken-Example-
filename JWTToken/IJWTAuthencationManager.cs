namespace JWTToken
{
    public interface IJWTAuthencationManager
    { 
        string Authenticate(string username, string password);
    }
}
