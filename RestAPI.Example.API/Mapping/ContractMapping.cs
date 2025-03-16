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
                Genres = request.Genres.ToList(),
            };
        }

        public static MovieResponse MapToMovieResponse(this Movie movie)
        {
            return new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Genres = movie.Genres
            };
        }

        public static MoviesResponse MapToMoviesResponse(this IEnumerable<Movie> movies)
        {
            return new MoviesResponse
            {
                Movies = movies.Select(MapToMovieResponse)
            };
        }

        public static Movie MapToMovie(this UpdateMovieRequest request, Guid id)
        {
            return new Movie
            {
                Id = id,
                Title = request.Title,
                YearOfRelease = request.YearOfRelease,
                Genres = request.Genres.ToList(),
            };
        }

        public static MovieResponse MapToMovieResponse(this UpdateMovieRequest updateMovieRequest, Guid id)
        {
            return new MovieResponse
            {
                Id = id,
                Title = updateMovieRequest.Title,
                YearOfRelease = updateMovieRequest.YearOfRelease,
                Genres = updateMovieRequest.Genres
            };
        }

        public static User MapToUser(this SignUpRequest signUpRequest)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = signUpRequest.Email,
                Password = signUpRequest.Password,
                Department = signUpRequest.Department
            };
        }
    }
}
