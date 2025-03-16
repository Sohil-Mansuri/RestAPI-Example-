using RestAPI.Example.Application.Models;

namespace RestAPI.Example.Application.Respositories
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(User user, CancellationToken cancellationToken = default);

        Task<User> GetUserByEmail(string email, CancellationToken cancellationToken = default);

        Task<IEnumerable<string>> GetUserRoles(Guid useId, CancellationToken cancellationToken = default);
    }
}
