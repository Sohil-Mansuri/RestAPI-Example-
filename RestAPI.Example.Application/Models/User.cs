

namespace RestAPI.Example.Application.Models
{
    public class User
    {
        public Guid Id { get; init; }

        public required string Email { get; init; }

        public required string Password { get; init; }

        public required string Department { get; init; }

        public string PasswordHash { get; set; }
    }
}
