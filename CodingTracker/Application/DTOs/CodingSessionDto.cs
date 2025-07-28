namespace CodingTracker.Application.DTOs;

public record CodingSessionDto(DateTime StartTime, DateTime? StopTime, TimeSpan Duration);