using AuthService.Dto;
using AuthService.Model;

namespace AuthService.Interface
{
    public interface IAuthService
    {
        Task<User> Register(Register register);
        Task<User> Login(Login login);
    }
}
