

using FluentValidation;
using RestAPI.Example.Application.Helper;
using RestAPI.Example.Application.Models;
using RestAPI.Example.Application.Respositories;
using RestAPI.Example.Contract.Request;
using RestAPI.Example.Contract.Response;

namespace RestAPI.Example.Application.Services
{
    public class UserService(IUserRepository userRepository, 
        IValidator<User> validator, 
        IPasswordHasher passwordHasher,
        TokenGenerator tokenGenerator) : IUserService
    {
        public async Task<object> Login(LoginRequest loginRequest, CancellationToken cancellationToken = default)
        {
            var currentUser = await userRepository.GetUserByEmail(loginRequest.Email, cancellationToken);

            if (currentUser is null) return null;

            var isValidUser = passwordHasher.Verify(currentUser.PasswordHash, loginRequest.Password);

            if (!isValidUser) return new LoginError("5000", "User is not valid, Please check password and try again");

            var token = tokenGenerator.GenerateToken(currentUser);
            return new LoginResponse(currentUser.Email, token, DateTime.Now.AddMinutes(20));
        }

        public async Task<bool> SignUp(User user, CancellationToken cancellationToken = default)
        {
            await validator.ValidateAndThrowAsync(user, cancellationToken);
            user.PasswordHash = passwordHasher.Hash(user.Password);
            return await userRepository.CreateUser(user, cancellationToken); 
        }
    }

}
