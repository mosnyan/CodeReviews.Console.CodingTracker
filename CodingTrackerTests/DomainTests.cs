using CodingTracker.Domain.Models;

namespace CodingTrackerTests;

public class DomainTests
{
    [Test]
    public void SessionSuccessfullyCreated()
    {
        Guid guid = Guid.NewGuid();
        DateTime start = DateTime.Now;
        DateTime stop = start.AddHours(3);
        CodingSession session = new(guid, start, stop);
        Assert.That(session.Duration.Hours == 3);
    }

    [Test]
    public void ThrowsOnStartTimeAfterStopTime()
    {
        Guid guid = Guid.NewGuid();
        DateTime start = DateTime.Now.AddHours(3);
        DateTime stop = DateTime.Now;
        Assert.Throws<ArgumentException>(() => new CodingSession(guid, start, stop));
    }
}