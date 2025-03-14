

using FluentValidation;
using RestAPI.Example.Application.Models;
using RestAPI.Example.Application.Respositories;
using RestAPI.Example.Application.Services;

namespace RestAPI.Example.Application.Validator
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        private readonly IMovieRepository _movieRepository;
        public MovieValidator(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;

            RuleFor(m => m.Id).NotEmpty();

            RuleFor(m => m.Title).NotEmpty();

            RuleFor(m => m.YearOfRelease)
                .LessThanOrEqualTo(DateTime.UtcNow.Year);

            RuleFor(m => m)
                .MustAsync(UniqueTitleYear)
                .WithMessage("A movie with the same Title and YearOfRelease already exists.");

        }

        private async Task<bool> UniqueTitleYear(Movie movie, CancellationToken cancellationToken)
        {
            var result = await _movieRepository.FindByName(movie.Title, cancellationToken);

            if (result.Count() > 0)
            {
                if (result.Any(m => m.YearOfRelease == movie.YearOfRelease)) return false;
            }

            return true;
        }
    }
}
