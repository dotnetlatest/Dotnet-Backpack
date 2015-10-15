namespace Backpack.WebApi.Security
{
    public interface IBasicSecurityService
    {
        bool SetPrincipal(string username, string password);
    }
}