
using RestAPI.Example.Application.Models;

namespace RestAPI.Example.Application.Services
{
    public interface IMovieService
    {
        Task<bool> CreateAsync(Movie movie, CancellationToken cancellationToken = default);

        Task<Movie> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Movie>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(Guid id, Movie movie, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Movie>> GetByName(string name, CancellationToken cancellationToken = default);
    }
}
