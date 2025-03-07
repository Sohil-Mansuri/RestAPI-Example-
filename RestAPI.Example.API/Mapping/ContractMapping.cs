using RestAPI.Example.Application.Models;
using RestAPI.Example.Contract.Request;
using RestAPI.Example.Contract.Response;

namespace RestAPI.Example.API.Mapping
{
    public static class ContractMapping
    {
        public static Movie MapToMovie(this CreateMovieRequest request)
        {
            return new Movie
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                YearOfRelease = request.YearOfRelease,
                Genres = request.Generes.ToList(),
            };
        }

        public static MovieResponse MapToMovieResponse(this Movie movie)
        {
            return new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Genera = movie.Genres
            };
        }

        public static MoviesResponse MapToMoviesResponse(this IEnumerable<Movie> movies)
        {
            return new MoviesResponse
            {
                Movies = movies.Select(MapToMovieResponse)
            };
        }
    }
}
