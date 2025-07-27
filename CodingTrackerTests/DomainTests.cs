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

    [Test]
    public void ReturnsGoodStringRepresentation()
    {
        var now = DateTime.Now;
        CodingSession session = new(
            now,
            now.AddHours(6)
        );

        var stringRep = $"Coding session ID: {session.Id}\n" +
                        $"Started at: {now}\n" +
                        $"Finished at: {now.AddHours(6)}\n" +
                        $"Duration: 6 hours\n";
        
        Assert.That(session.ToString(), Is.EqualTo(stringRep));
    }
}