using CodingTracker.Application.Abstractions.Repositories;
using CodingTracker.Application.DTOs;
using CodingTracker.Domain.Models;

namespace CodingTracker.Application.Services;

public class SessionService(ISessionRepository repository)
{
    
    public bool CreateSession(CodingSessionDto dto)
    {
        return repository.CreateSession(MapToModel(dto));
    }
    
    public IEnumerable<CodingSessionDto> ReadAllSessions()
    {
        return repository.ReadSessions()
            .Select(MapToDto);
    }

    public IEnumerable<CodingSessionDto> ReadAllIncompleteSessions()
    {
        return repository.ReadSessions()
            .Where(session => !session.Completed)
            .Select(MapToDto);
    }

    public CodingSessionDto ReadSessionById(Guid id)
    {
        var session = repository.ReadSessionById(id);

        if (session is null)
        {
            throw new InvalidOperationException($"No session retrieved with id {id}");
        }

        return MapToDto(session);
    }

    public bool UpdateSession(CodingSessionDto dto)
    {
        return repository.UpdateSession(MapToModel(dto));
    }

    public bool DeleteSession(CodingSessionDto dto)
    {
        return repository.DeleteSession(MapToModel(dto));
    }

    private CodingSessionDto MapToDto(CodingSession session)
    {
        return new CodingSessionDto(session.StartTime,
                                    session.StopTime,
                                    session.Duration);
    }

    private CodingSession MapToModel(CodingSessionDto dto)
    {
        return new CodingSession(dto.StartTime,
                                 dto.StopTime);
    }
}