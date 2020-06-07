namespace JusTalk.Web.Contracts.v1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = "/" + Root + "/" + Version;

        public static class Identity
        {
            public const string Login = Base + "/login";

            public const string Confirm = Base + "/confirm";
        }
        
        public static class Profile
        {
            public const string Index = Base + "/profile";
            
            public const string Show = Base + "/profile/{id}";
            
            public const string Update = Base + "/profile";
        }
        
        public static class Search
        {
            public const string Index = Base + "/search";
        }
    }
}