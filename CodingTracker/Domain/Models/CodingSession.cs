namespace CodingTracker.Domain.Models;

public class CodingSession
{
    public Guid Id { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime StopTime { get; private set; }
    public TimeSpan Duration => StopTime - StartTime;
    public long GetStartTimestamp() => (long) (StartTime - DateTime.UnixEpoch).TotalMilliseconds;
    public long GetStopTimeTimestamp() => (long)(StopTime - DateTime.UnixEpoch).TotalMilliseconds;

    public CodingSession(DateTime startTime, DateTime stopTime)
    {
        Id = Guid.NewGuid();
        StartTime = startTime;
        StopTime = stopTime;
        if (Duration < TimeSpan.Zero)
        {
            throw new ArgumentException("Start time is after stop time!");
        }
    }

    public CodingSession(Guid id, DateTime startTime, DateTime stopTime)
    {
        Id = id;
        StartTime = startTime;
        StopTime = stopTime;
        if (Duration < TimeSpan.Zero)
        {
            throw new ArgumentException("Start time is after stop time!");
        }
    }

    public static bool operator ==(CodingSession c1, CodingSession c2)
    {
        return c1.Id == c2.Id;
    }

    public static bool operator !=(CodingSession c1, CodingSession c2)
    {
        return c1.Id != c2.Id;
    }
}