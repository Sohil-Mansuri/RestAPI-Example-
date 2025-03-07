using RestAPI.Example.Application.Models;

namespace RestAPI.Example.Application.Respositories
{
    public class MovieRepository : IMovieRepository
    {
        private List<Movie> _movies = [];
        public Task<bool> CreateAsync(Movie movie)
        {
            _movies.Add(movie);
            return Task.FromResult(true);
        }

        public Task<Movie?> GetByIdAsync(Guid id)
        {
            var movie = _movies.FirstOrDefault(movies => movies.Id == id);
            return Task.FromResult(movie);
        }

        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            return Task.FromResult(_movies.AsEnumerable());
        }

        public Task<bool> UpdateAsync(Guid id, Movie movie)
        {
            var movieIndex = _movies.FindIndex(movie => movie.Id == id);
            if (movieIndex == -1)
            {
                return Task.FromResult(false);
            }

            _movies[movieIndex] = movie;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var movie = _movies.RemoveAll(movie => movie.Id == id);
            bool movieRemoved = movie > 0;
            return Task.FromResult(movieRemoved);
        }
    }
}
