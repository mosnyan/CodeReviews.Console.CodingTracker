using CodingTracker.Application.DTOs;

namespace CodingTracker.UI.Abstractions;

public interface IView
{
    public void DisplayHeader();
    public string GetMainMenuOptions(IEnumerable<string> options);
    public DateTime GetOngoingSessionInfo();
    public (DateTime, DateTime) GetCompletedSessionInfo();
    public int GetSessionIndex(IEnumerable<CodingSessionDto> sessions);
    public DateTime CompleteSession();
    public void DisplayTime(TimeSpan time);
    public bool StopStopwatch();
    public void DisplaySessions(IEnumerable<CodingSessionDto> sessions);
    public void DisplaySession(CodingSessionDto session);
    public void DisplayMessage(string message);
}