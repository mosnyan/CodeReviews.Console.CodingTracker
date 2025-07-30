using CodingTracker.Application.Abstractions.Repositories;
using CodingTracker.Application.DTOs;
using CodingTracker.Domain.Models;

namespace CodingTracker.Application.Services;

public class SessionService(ISessionRepository repository)
{
    
    public bool CreateOngoingSession(OngoingCodingSessionCreationDto dto)
    {
        var session = new CodingSession(dto.StartTime);
        return repository.CreateSession(session);
    }

    public bool CreateCompletedSession(CompletedCodingSessionCreationDto dto)
    {
        var session = new CodingSession(dto.StartTime, dto.StopTime);
        return repository.CreateSession(session);
    }
    
    public IEnumerable<CodingSessionDto> ReadAllSessions()
    {
        return repository.ReadAllSessions()
            .Select(MapToDto);
    }

    public IEnumerable<CodingSessionDto> ReadAllIncompleteSessions()
    {
        return repository.ReadAllSessions()
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

    public bool UpdateSession(CodingSessionDto editionDto)
    {
        var session = new CodingSession(
            editionDto.Id,
            editionDto.StartTime,
            editionDto.StopTime
            );
        
        return repository.UpdateSession(session);
    }

    public bool DeleteSession(Guid id)
    {
        return repository.DeleteSessionById(id);
    }

    private CodingSessionDto MapToDto(CodingSession session)
    {
        return new CodingSessionDto(session.Id,
                                    session.StartTime,
                                    session.StopTime,
                                    session.Duration);
    }
    
}