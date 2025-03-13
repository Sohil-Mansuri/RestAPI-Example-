using RestAPI.Example.Application.Models;
using RestAPI.Example.Application.Respositories;

namespace RestAPI.Example.Application.Services
{
    public class MovieService(IMovieRepository movieRepository) : IMovieService
    {
        public Task<bool> CreateAsync(Movie movie)
        {
            return movieRepository.CreateAsync(movie);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return movieRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            return movieRepository.GetAllAsync();
        }

        public Task<Movie?> GetByIdAsync(Guid id)
        {
            return movieRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Movie>> GetByName(string name)
        {
            return await movieRepository.FindByName(name);
        }

        public async Task<bool> UpdateAsync(Guid id, Movie movie)
        {
            var movieExist = await movieRepository.IsMovieExist(id);
            if (movieExist)
            {
                return await movieRepository.UpdateAsync(id, movie);
            }

            return false;
        }
    }
}
