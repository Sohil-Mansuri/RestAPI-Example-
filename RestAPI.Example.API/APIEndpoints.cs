﻿namespace RestAPI.Example.API
{
    public static class APIEndpoints
    {
        public const string APIBase = "api";

        public static class Movie
        {
            public const string Base = $"{APIBase}/movies";

            public const string Create = Base;

            public const string Get = $"{Base}/{{id:guid}}";

            public const string GetAll = Base;

            public const string Update = $"{Base}/{{id:guid}}";

            public const string Delete = Get;

            public const string GetByName = $"{Base}/getByName";    
        }

        public static class User
        {
            public const string Base = $"{APIBase}/user";

            public const string Login = $"{Base}/login";

            public const string SignUp = $"{Base}/signUp";

        }
    }
}
