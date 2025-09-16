namespace TodoApi.Core
{
    public static class TodoCategory
    {
        public const string Setup = "Setup";
        public const string Development = "Development";
        public const string Documentation = "Documentation";
        public const string Testing = "Testing";
        public const string Deployment = "Deployment";
        public const string Maintenance = "Maintenance";
        public const string Research = "Research";
        public const string Planning = "Planning";
        public const string Review = "Review";
        public const string Other = "Other";

        public static readonly string[] AllCategories = {
            Setup, Development, Documentation, Testing, Deployment,
            Maintenance, Research, Planning, Review, Other
        };
    }
}
