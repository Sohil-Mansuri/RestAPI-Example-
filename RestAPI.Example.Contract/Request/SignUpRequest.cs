namespace RestAPI.Example.Contract.Request
{
    public class SignUpRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Department { get; set; }
    }
}
