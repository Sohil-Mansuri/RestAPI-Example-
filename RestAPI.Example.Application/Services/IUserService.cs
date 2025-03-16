

using RestAPI.Example.Application.Models;
using RestAPI.Example.Contract.Request;
using RestAPI.Example.Contract.Response;

namespace RestAPI.Example.Application.Services
{
    public interface IUserService
    {
        Task<object> Login(LoginRequest loginRequest, CancellationToken cancellationToken = default);

        Task<bool> SignUp(User user, CancellationToken cancellationToken = default);
    }

}
