using System.Text;

namespace CodingTracker.Application.DTOs;

public record CodingSessionDto(Guid Id, DateTime StartTime, DateTime? StopTime, TimeSpan Duration)
{
  public override string ToString()
  {
    var sb = new StringBuilder();
    sb.AppendLine($"Started at: {StartTime:yyyy/MM/dd HH:mm:ss}");
    if (StopTime is not null)
    {
      sb.AppendLine($"Finished at: {StopTime:yyyy/MM/dd HH:mm:ss}");
    }
    else
    {
      sb.AppendLine("Not finished yet.");
    }
    sb.AppendLine($"Duration: {Duration:ddd' days 'hh' hrs 'mm' mins 'ss' secs '}");
    return sb.ToString();
  }
}