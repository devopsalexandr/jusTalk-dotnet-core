namespace JusTalk.Web.Contracts.v1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = "/" + Root + "/" + Version;

        public static class Authentication
        {
            public const string Login = Base + "/login";

            public const string Confirm = Base + "/confirm";
        }
    }
}