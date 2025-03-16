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

            //add api role also
            var apiRoleId = await connection.QuerySingleOrDefaultAsync<Guid>(@"select Id from Roles where Name = 'APIUser'");
            await connection.ExecuteAsync
                (new CommandDefinition(@"insert into UserRoles (UserId, RoleId) values(@id, @apiRoleId)", new {id = user.Id, apiRoleId}));

            return result > 0;
        }

        public async Task<User> GetUserByEmail(string email, CancellationToken cancellationToken = default)
        {
            using var connection = await dBConnectionFactory.CreateAsync(cancellationToken);
            var user = await connection.QuerySingleOrDefaultAsync<User>
                (new CommandDefinition(@"select Id, Email, PasswordHash from Users where Email = @email", new { email }, cancellationToken: cancellationToken));

            return user;
        }

        public async Task<IEnumerable<string>> GetUserRoles(Guid userId, CancellationToken cancellationToken = default)
        {
            using var connection = await dBConnectionFactory.CreateAsync(cancellationToken);

            var result = await connection.QueryAsync<string>
                (new CommandDefinition(@"select r.Name from Roles r join UserRoles u on u.RoleId = r.Id and u.UserId = @userId", new { userId }));

            return result;
        }
    }
}
