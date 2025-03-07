using RestAPI.Example.Application.Models;

namespace RestAPI.Example.Application.Respositories
{
    public interface IMovieRepository
    {
        Task<bool> CreateAsync(Movie movie);

        Task<Movie?> GetByIdAsync(Guid id);

        Task<IEnumerable<Movie>> GetAllAsync();

        Task<bool> UpdateAsync(Guid id, Movie movie);

        Task<bool> DeleteAsync(Guid id);
    }
}
