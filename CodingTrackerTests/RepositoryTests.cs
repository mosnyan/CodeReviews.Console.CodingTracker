using System.Data.SQLite;
using CodingTracker.Domain.Models;
using CodingTracker.Infrastructure.Repositories;
using Dapper;

namespace CodingTrackerTests;

public class RepositoryTests
{
    private const string ConnectionString = "Data Source = testcodingdb";
    private SessionRepository _sessionRepo = new(ConnectionString);

    [SetUp]
    public void SetUp()
    {
        using var connection = new SQLiteConnection(ConnectionString);
        var query = "DELETE FROM sessions;";
        connection.Execute(query);
    }

    [Test]
    public void CreateSession()
    {
        CodingSession session = new(
            Guid.NewGuid(),
            DateTime.Now,
            DateTime.Now.AddHours(3)
        );

        Assert.That(_sessionRepo.CreateSession(session), Is.True);
    }
    
    [Test]
    public void GetAllSessions()
    {
        CodingSession session = new(
            DateTime.Now,
            DateTime.Now.AddHours(3)
        );

        _ = _sessionRepo.CreateSession(session);
        var sessions = _sessionRepo.ReadAllSessions().ToList();
        
        Assert.That(sessions.Count, Is.EqualTo(1));
    }

    [Test]
    public void GetOneSession()
    {
        var now = DateTime.Now;
        CodingSession session = new(
            now,
            now.AddHours(3)
        );

        _ = _sessionRepo.CreateSession(session);
        var readBack = _sessionRepo.ReadSessionById(session.Id);
        
        Assert.That(session == readBack);
    }

    [Test]
    public void UpdateSession()
    {
        CodingSession session = new(
            DateTime.Now,
            DateTime.Now.AddHours(3)
        );

        _ = _sessionRepo.CreateSession(session);

        var readSession = _sessionRepo.ReadSessionById(session.Id)!;

        CodingSession updatedSession = new(
            readSession.Id,
            readSession.StartTime,
            readSession.StopTime!.Value.AddHours(2)
        );

        _ = _sessionRepo.UpdateSession(updatedSession);

        var readBack = _sessionRepo.ReadSessionById(session.Id)!;
        
        Assert.That(session == readBack);
        Assert.That(readBack.Duration.Hours, Is.EqualTo(5));
    }

    [Test]
    public void DeleteSession()
    {
        CodingSession session = new(
            DateTime.Now,
            DateTime.Now.AddHours(3)
        );

        _ = _sessionRepo.CreateSession(session);
        
        Assert.That(_sessionRepo.DeleteSession(session), Is.True);
    }
}