using CodingTracker.Domain.Models;

namespace CodingTracker.Application.Abstractions.Repositories;

public interface ISessionRepository
{
    public bool CreateSession(CodingSession session);
    public IEnumerable<CodingSession> ReadAllSessions();
    public CodingSession? ReadSessionById(Guid id);
    public bool UpdateSession(CodingSession session);
    public bool DeleteSession(CodingSession session);
    public bool DeleteSessionById(Guid id);
}