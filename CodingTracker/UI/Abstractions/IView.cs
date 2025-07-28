using CodingTracker.Application.DTOs;

namespace CodingTracker.UI.Abstractions;

public interface IView
{
    public void DisplayHeader();
    public void DisplayMainMenuOptions(IEnumerable<string> options);
    public DateTime GetOngoingSessionInfo();
    public (DateTime, DateTime) GetCompletedSessionInfo();
    public DateTime GetSessionCompletionTime();
    public void DisplaySessions(IEnumerable<CodingSessionDto> habits);
    public void DisplaySession(CodingSessionDto session);
    public int GetUserChoice();
    public void DisplayMessage(string message);
}