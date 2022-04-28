namespace Infrastructure.MyDbContext
{
    public static class DatabaseSettings
    {
        public static string ConnectionString { get; } = "mongodb://localhost:27017";
        public static string DatabaseName { get; } = "internet_shop_archive";
        public static string Logs { get; } = "logs";
        public static string OrderingHistory { get; } = "ordering_history";
    }
}