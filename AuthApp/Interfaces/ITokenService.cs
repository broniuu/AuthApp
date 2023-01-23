using AuthApp.Model;

namespace AuthApp.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}