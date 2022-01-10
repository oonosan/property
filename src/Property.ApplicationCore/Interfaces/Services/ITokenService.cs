using Property.ApplicationCore.Entities;

namespace Property.ApplicationCore.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
