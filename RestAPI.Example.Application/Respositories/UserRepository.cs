using Dapper;
using RestAPI.Example.Application.Database;
using RestAPI.Example.Application.Models;

namespace RestAPI.Example.Application.Respositories
{
    public class UserRepository(IDBConnectionFactory dBConnectionFactory) : IUserRepository
    {
        public async Task<bool> CreateUser(User user, CancellationToken cancellationToken = default)
        {
            using var connection = await dBConnectionFactory.CreateAsync(cancellationToken);
            var result = await connection.ExecuteAsync
                (new CommandDefinition(@"insert into Users (Id, Email, PasswordHash, Department) values(@id, @email, @password, @department)", new
                {
                    id = user.Id,
                    email = user.Email,
                    password = user.PasswordHash,
                    department = user.Department,
                }, cancellationToken: cancellationToken));

            return result > 0;
        }

        public async Task<User> GetUserByEmail(string email, CancellationToken cancellationToken = default)
        {
            using var connection = await dBConnectionFactory.CreateAsync(cancellationToken);
            var user = await connection.QuerySingleOrDefaultAsync<User>
                (new CommandDefinition(@"select Id, Email, PasswordHash from Users where Email = @email", new { email }, cancellationToken: cancellationToken));

            return user;
        }
    }
}
