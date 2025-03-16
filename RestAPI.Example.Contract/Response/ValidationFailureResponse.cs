namespace RestAPI.Example.Contract.Response
{
    public record ValidationFailureResponse(IEnumerable<ValidationResponse> Errors);


    public record ValidationResponse(string PropertyName, string Message);

}
