﻿namespace RestAPI.Example.Contract.Response
{
    public class MovieResponse
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required int YearOfRelease { get; init; }
        public IEnumerable<string> Genres { get; init; } = [];

    }
}
