using System.Data.SQLite;
using CodingTracker.Application.Abstractions.Repositories;
using CodingTracker.Domain.Models;
using Dapper;

namespace CodingTracker.Infrastructure.Repositories;

public class SessionRepository(string connectionString) : ISessionRepository
{
    public bool CreateSession(CodingSession session)
    {
        using var connection = new SQLiteConnection(connectionString);
        var query = "INSERT INTO sessions (id, start_time, stop_time) VALUES (@Id, @StartTime, @StopTime)";
        return SqlMapper.Execute(connection, query, MapToDatabase(session)) > 0; 
    }

    public IEnumerable<CodingSession> ReadAllSessions()
    {
        using var connection = new SQLiteConnection(connectionString);
        var query = "SELECT * FROM sessions";
        var sessions = connection.Query(query);


        return sessions
            .Select(MapFromDatabase);
    }

    public CodingSession? ReadSessionById(Guid id)
    {
        using var connection = new SQLiteConnection(connectionString);
        var query = "SELECT * FROM sessions WHERE id = @Id";
        
        return connection.Query(query,
                new
                {
                    Id = id.ToString()
                })
            .Select(MapFromDatabase)
            .SingleOrDefault();
    }

    public bool UpdateSession(CodingSession session)
    {
        using var connection = new SQLiteConnection(connectionString);
        var query = "UPDATE sessions SET start_time = @StartTime, stop_time = @StopTime WHERE id = @Id";

        return SqlMapper.Execute(connection, query, MapToDatabase(session)) > 0; 
    }

    public bool DeleteSession(CodingSession session)
    {
        using var connection = new SQLiteConnection(connectionString);
        var query = "DELETE FROM sessions WHERE id = @Id";

        return connection.Execute(query,
            new
            {
                Id = session.Id.ToString()
            }) > 0;
    }

    public bool DeleteSessionById(Guid id)
    {
        using var connection = new SQLiteConnection(connectionString);
        var query = "DELETE FROM sessions WHERE id = @Id";

        return connection.Execute(query,
            new
            {
                Id = id.ToString()
            }) > 0;
    }

    private CodingSession MapFromDatabase(dynamic row)
    {
        return new CodingSession(
            Guid.Parse(row.id),
            DateTime.UnixEpoch.AddSeconds(row.start_time),
            row.stop_time is null ? null : DateTime.UnixEpoch.AddSeconds(row.stop_time)
        );
    }

    private dynamic MapToDatabase(CodingSession session)
    {
        return new
        {
            Id = session.Id.ToString(),
            StartTime = (long)(session.StartTime - DateTime.UnixEpoch).TotalSeconds,
            StopTime = session.StopTime is null ?
                (long?)null : (long)(session.StopTime!.Value - DateTime.UnixEpoch).TotalSeconds
        };
    }
    
}