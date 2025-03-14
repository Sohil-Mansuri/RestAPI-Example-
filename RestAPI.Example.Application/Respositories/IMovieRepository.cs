using RestAPI.Example.Application.Models;

namespace RestAPI.Example.Application.Respositories
{
    public interface IMovieRepository
    {
        Task<bool> CreateAsync(Movie movie, CancellationToken cancellationToken = default);

        Task<Movie> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Movie>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(Guid id, Movie movie, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Movie>> FindByName(string name, CancellationToken cancellationToken = default);
        
        Task<bool> IsMovieExist(Guid id, CancellationToken cancellationToken = default);
    }
}
