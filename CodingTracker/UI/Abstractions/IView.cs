using CodingTracker.Application.DTOs;

namespace CodingTracker.UI.Abstractions;

public interface IView
{
    public void DisplayHeader();
    public string GetMenuOption(IReadOnlyList<string> options);
    public DateTime GetOngoingSessionInfo();
    public (DateTime, DateTime) GetCompletedSessionInfo();
    public int GetSessionIndex(IReadOnlyList<CodingSessionDto> sessions);
    public DateTime CompleteSession();
    public void DisplayStopwatchTime(TimeSpan time);
    public bool StopStopwatch();
    public void DisplaySessions(IReadOnlyList<CodingSessionDto> sessions);
    public void DisplaySession(CodingSessionDto session);
    public void DisplayCodingTime(TimeSpan time, string timeSpan);
    public void DisplayMessage(string message);
}