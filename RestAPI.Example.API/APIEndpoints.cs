namespace RestAPI.Example.API
{
    public static class APIEndpoints
    {
        public const string APIBase = "api";

        public static class Movie
        {
            public const string Base = $"{APIBase}/movies";

            public const string Create = Base;

            public const string Get = $"{Base}/{{id}}";

            public const string GetAll = Base;
        }
    }
}
