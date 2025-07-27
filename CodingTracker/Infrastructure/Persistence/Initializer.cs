using Dapper;
using System.Data.SQLite;

namespace CodingTracker.Infrastructure.Persistence;

public class Initializer(string connectionString)
{

    public void Initialize()
    {
        using var connection = new SQLiteConnection(connectionString);
        CreateCodingSessionsTable(connection);
    }

    private void CreateCodingSessionsTable(SQLiteConnection connection)
    {
        string query = "CREATE TABLE IF NOT EXISTS sessions" +
                       "(id TEXT PRIMARY KEY," +
                       "start_time INTEGER NOT NULL," +
                       "stop_time INTEGER NOT NULL)";
        connection.Execute(query);
    }
}