namespace KoksyApp.API.Settings;

public class DatabaseSettings
{
    public DatabaseSettings(string? connectionString, string? databaseName)
    {
        ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        DatabaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
    }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}