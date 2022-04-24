namespace TrackFinances.Api.Contracts;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = Root + "/" + Version;

    public static class User
    {
        public const string Get = Base + "/user/{userId}";
        public const string Update = Base + "/user/{userId}";
        public const string Delete = Base + "/user/{userId}";
        public const string GetBy = Base + "/user";
        public const string Create = Base + "/user";
    }
}
