

using Dapper;

namespace RestAPI.Example.Application.Database
{
    public class DBInitilizer(IDBConnectionFactory dBConnectionFactory)
    {
        public async Task Initilizer()
        {
            using var connection = await dBConnectionFactory.CreateAsync();

            await connection.ExecuteAsync(@"

                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Movie')
                BEGIN
                    CREATE TABLE Movie (
                        Id UNIQUEIDENTIFIER PRIMARY KEY,
                        Title NVARCHAR(255) NOT NULL,
                        YearOfRelease INT NOT NULL
                    );
                END

                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Genre')
                BEGIN
                    CREATE TABLE Genre (
                        MovieId UNIQUEIDENTIFIER NOT NULL,
                        Type NVARCHAR(100) NOT NULL,
                        FOREIGN KEY (MovieId) REFERENCES Movie(Id) ON DELETE CASCADE
                    );
                END

            ");

            //create index
            await connection.ExecuteAsync(@"

                IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Movie_Title' AND object_id = OBJECT_ID('Movie'))
                BEGIN
                    CREATE INDEX IX_Movie_Title ON Movie(Title);
                END
                ");

            //create User 

            await connection.ExecuteAsync(@"
                
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users')
                   BEGIN
                    CREATE TABLE Users (
                    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                    Email NVARCHAR(255) NOT NULL UNIQUE,
                    PasswordHash NVARCHAR(1000) NOT NULL,
                    Department NVARCHAR(255) NOT NULL);
                   END
                ");

        }
    }
}
