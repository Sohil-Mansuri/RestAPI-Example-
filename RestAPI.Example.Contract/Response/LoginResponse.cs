

namespace RestAPI.Example.Contract.Response
{
    public record LoginResponse(string Email, string AccessToken, DateTime expiryTime);

    public record LoginError(string ErrorCode, string ErrorMessage);
    
}
