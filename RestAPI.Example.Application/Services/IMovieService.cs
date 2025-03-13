
using RestAPI.Example.Application.Models;

namespace RestAPI.Example.Application.Services
{
    public interface IMovieService
    {
        Task<bool> CreateAsync(Movie movie);

        Task<Movie?> GetByIdAsync(Guid id);

        Task<IEnumerable<Movie>> GetAllAsync();

        Task<bool> UpdateAsync(Guid id, Movie movie);

        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<Movie>> GetByName(string name);
    }
}
