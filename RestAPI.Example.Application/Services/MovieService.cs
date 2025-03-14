using FluentValidation;
using RestAPI.Example.Application.Models;
using RestAPI.Example.Application.Respositories;

namespace RestAPI.Example.Application.Services
{
    public class MovieService(IMovieRepository movieRepository, IValidator<Movie> movieValidator) : IMovieService
    {
        public async Task<bool> CreateAsync(Movie movie, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await movieValidator.ValidateAndThrowAsync(movie, cancellationToken);
            return await movieRepository.CreateAsync(movie, cancellationToken);
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return movieRepository.DeleteAsync(id, cancellationToken);
        }

        public Task<IEnumerable<Movie>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return movieRepository.GetAllAsync(cancellationToken);
        }

        public Task<Movie> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return movieRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Movie>> GetByName(string name, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await movieRepository.FindByName(name, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Guid id, Movie movie, CancellationToken cancellationToken = default )
        {
            cancellationToken.ThrowIfCancellationRequested();

            await movieValidator.ValidateAndThrowAsync(movie, cancellationToken);
            var movieExist = await movieRepository.IsMovieExist(id, cancellationToken);
            if (movieExist)
            {
                return await movieRepository.UpdateAsync(id, movie, cancellationToken);
            }

            return false;
        }
    }
}
