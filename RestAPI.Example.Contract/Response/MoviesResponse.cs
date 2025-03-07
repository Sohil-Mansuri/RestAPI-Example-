namespace RestAPI.Example.Contract.Response
{
    public class MoviesResponse
    {
        public required IEnumerable<MovieResponse> Movies { get; set; } = [];
    }
}
