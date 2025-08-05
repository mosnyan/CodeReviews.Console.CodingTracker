using System.Text;

namespace CodingTracker.Domain.Models;

public class CodingSession
{
    public Guid Id { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime? StopTime { get; init; }

    public TimeSpan Duration => StopTime is null ? DateTime.Now - StartTime : StopTime.Value - StartTime;

    public bool Completed => StopTime is not null;

    public CodingSession(DateTime startTime)
    {
        Id = Guid.NewGuid();
        StartTime = startTime;
        StopTime = null;
        ValidateInvariants();
    }
    
    public CodingSession(DateTime startTime, DateTime? stopTime)
    {
        Id = Guid.NewGuid();
        StartTime = startTime;
        StopTime = stopTime;
        ValidateInvariants();
    }

    public CodingSession(Guid id, DateTime startTime, DateTime? stopTime)
    {
        Id = id;
        StartTime = startTime;
        StopTime = stopTime;
        ValidateInvariants();
    }

    private void ValidateInvariants()
    {
        if (StartTime > DateTime.Now)
        {
            throw new ArgumentException("Start time is in the future!");
        }

        if (StopTime > DateTime.Now)
        {
            throw new ArgumentException("Stop time is in the future!");
        }
        
        if (Duration < TimeSpan.Zero)
        {
            throw new ArgumentException("Start time is after stop time!");
        }  
    }

    public static bool operator ==(CodingSession? c1, CodingSession? c2)
    {
        if (c1 is null)
        {
            return c2 is null;
        }

        return c1.Equals(c2);
    }

    public static bool operator !=(CodingSession? c1, CodingSession? c2)
    {
        if (c1 is null)
        {
            return c2 is not null;
        }

        return !c1.Equals(c2);
    }

    public override bool Equals(object? obj)
    {
        if (obj is CodingSession other)
        {
            return Id == other.Id;
        }

        return false;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Coding session ID: {Id}");
        sb.AppendLine($"Started at: {StartTime}");
        if (Completed)
        {
            sb.AppendLine($"Finished at: {StopTime}");
        }
        else
        {
            sb.AppendLine("Not finished yet.");
        }
        sb.AppendLine($"Duration: {Duration.Hours} hours");
        return sb.ToString();
    }

    public override int GetHashCode() => Id.GetHashCode();
}