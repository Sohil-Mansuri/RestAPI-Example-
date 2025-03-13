using Dapper;
using RestAPI.Example.Application.Database;
using RestAPI.Example.Application.Models;

namespace RestAPI.Example.Application.Respositories
{
    public class MovieRepository(IDBConnectionFactory dBConnectionFactory) : IMovieRepository
    {
        public async Task<bool> CreateAsync(Movie movie)
        {
            using var connection = await dBConnectionFactory.CreateAsync();
            using var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync(new
                CommandDefinition(@"insert into Movie (Id, Title, YearOfRelease) values(@Id, @Title, @YearOfRelease)", movie, transaction));

            if (result > 0)
            {
                foreach (var genre in movie.Genres)
                {
                    await connection.ExecuteAsync
                        (new CommandDefinition(@"insert into Genre (MovieId, Type) values(@Id, @Type)", new { movie.Id, Type = genre }, transaction));
                }
            }

            transaction.Commit();

            return result > 0;
        }

        public async Task<Movie?> GetByIdAsync(Guid id)
        {
            using var connection = await dBConnectionFactory.CreateAsync();

            var movie = await connection.QuerySingleOrDefaultAsync<Movie>
                (new CommandDefinition(@"select Id, Title, YearOfRelease from Movie where Id = @Id", new { Id = id }));

            if (movie is null) return null;

            var genres = await connection.QueryAsync<string>
                (new CommandDefinition(@"select Type from Genre where MovieId = @Id", new { Id = id }));

            foreach (var genre in genres)
            {
                movie.Genres.Add(genre);
            }

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            using var connection = await dBConnectionFactory.CreateAsync();

            var result = await connection.QueryAsync
                (new CommandDefinition(@"select m.*, string_agg(g.Type, ',') Genres from Movie m left join Genre g on m.Id = g.MovieId group by Id, Title, YearOfRelease"));

            return result.Select(m => new Movie
            {
                Id = m.Id,
                Title = m.Title,
                YearOfRelease = m.YearOfRelease,
                Genres = Enumerable.ToList(m.Genres.Split(','))
            });
        }

        public async Task<bool> UpdateAsync(Guid id, Movie movie)
        {
            using var connection = await dBConnectionFactory.CreateAsync();
            using var transaction = connection.BeginTransaction();
            await connection.ExecuteAsync(new CommandDefinition(@"delete from Genre where MovieId = @id", new { id }, transaction));

            foreach (var genre in movie.Genres)
            {
                await connection.ExecuteAsync
                    (new CommandDefinition(@"insert into Genre (MovieId, Type) values(@movieId, @type)",
                    new { movieId = id, type = genre }, transaction));
            }

            var result = await connection.ExecuteAsync
                (new CommandDefinition(@"update Movie set Title = @title, YearOfRelease = @yeaOfRelease where Id = @id",
                new { title = movie.Title, yeaOfRelease = movie.YearOfRelease, id }, transaction));

            transaction.Commit();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = await dBConnectionFactory.CreateAsync();

            var result = await connection.ExecuteAsync
                (new CommandDefinition(@"delete from Movie where Id = @id", new {id}));

            return result > 0;
        }

        public async Task<IEnumerable<Movie>> FindByName(string name)
        {
            using var connection = await dBConnectionFactory.CreateAsync();

            var result = await connection.QueryAsync<Movie>
                (new CommandDefinition(@"select * from Movie where Title = @name", new {name}));

            return result;
        }

        public async Task<bool> IsMovieExist(Guid id)
        {
            using var connection = await dBConnectionFactory.CreateAsync();

            return await connection.ExecuteScalarAsync<bool>
                (new CommandDefinition(@"select count(1) from Movie where Id = @id", new { id }));
        }
    }
}
