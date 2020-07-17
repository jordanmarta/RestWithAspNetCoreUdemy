using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Models;

namespace RestWithAspNetCoreUdemy.Services.Interfaces
{
    public interface ILoginService
    {
        object FindByLogin(UserVO login);
    }
}
