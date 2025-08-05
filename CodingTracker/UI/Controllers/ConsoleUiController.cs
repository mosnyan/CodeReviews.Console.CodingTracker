using CodingTracker.Application.DTOs;
using CodingTracker.Application.Services;
using CodingTracker.UI.Abstractions;

namespace CodingTracker.UI.Controllers;

public class ConsoleUiController(SessionService service,
                                 IView view)                              
{
    private readonly List<string> _features =
    [
        "1. Create new ongoing session",
        "2. Create new completed session",
        "3. Stopwatch session",
        "4. See all sessions",
        "5. Session reports",
        "6. Edit session",
        "7. Delete session",
        "8. Exit application"
    ];

    private readonly List<string> _reports =
    [
        "1. Today",
        "2. This week",
        "3. This month",
        "4. This year",
        "5. Cancel"
    ];

    private volatile bool _stopwatchStopped = true;

    public bool Running { get; private set; }
    
    public void Initialize()
    {
        view.DisplayHeader();
        Running = true;
    }

    public void Execute()
    {
        var choice = view.GetMenuOption(_features);

        switch (choice)
        {
            case var _ when choice == _features[0]:
                HandleCreatingOngoingSession();
                break;
            case var _ when choice == _features[1]:
                HandleCreatingCompletedSession();
                break;
            case var _ when choice == _features[2]:
                HandleStopwatch();
                break;
            case var _ when choice == _features[3]:
                HandleSeeAllSessions();
                break;
            case var _ when choice == _features[4]:
                HandleReports();
                break;
            case var _ when choice == _features[5]:
                HandleEditSession();
                break;
            case var _ when choice == _features[6]:
                HandleDeleteSession();
                break;
            case var _ when choice == _features[7]:
                HandleExit();
                break;
        }
    }

    private void HandleCreatingOngoingSession()
    {
        var startTime = view.GetOngoingSessionInfo();
        OngoingCodingSessionCreationDto dto = new(startTime);
        
        try
        {
            service.CreateOngoingSession(dto);
        }
        catch (ArgumentException e)
        {
            view.DisplayMessage(e.Message);
        }
    }

    private void HandleCreatingCompletedSession()
    {
        var (startTime, stopTime) = view.GetCompletedSessionInfo();
        CompletedCodingSessionCreationDto dto = new(startTime, stopTime);

        try
        {
            service.CreateCompletedSession(dto);
        }
        catch (ArgumentException e)
        {
            view.DisplayMessage(e.Message);
        }
    }

    private void HandleStopwatch()
    {
        // Initialize stopwatch : put status to not stopped (running)
        // and build the thread with the stopwatch method
        // The thread isn't necessary, but I wanted to see how they worked in C#
        // I also thought that with Spectre I could have a header display the running time while the user
        // does something else, to no avail because most input operations are blocking
        _stopwatchStopped = false;
        Thread stopwatch = new Thread(Stopwatch);
        
        // Log current time as starting time
        var startTime = DateTime.Now;
        
        // Start thread
        stopwatch.Start();
        
        // View will stop the stopwatch once the user has had enough
        _stopwatchStopped = view.StopStopwatch();
        
        // Log current time as stopping time
        var stopTime = DateTime.Now;

        // Create new session
        var session = new CompletedCodingSessionCreationDto(startTime, stopTime);

        service.CreateCompletedSession(session);
    }

    private void Stopwatch()
    {
        var then = DateTime.Now;
        while (true)
        {
            if (_stopwatchStopped)
            {
                break;
            }
            view.DisplayStopwatchTime(DateTime.Now - then);
            Thread.Sleep(250);
        }
    }

    private void HandleSeeAllSessions()
    {
        var sessions = service.ReadAllSessions().ToList();
        view.DisplaySessions(sessions);
    }
    
    private void HandleReports()
    {
        var choice = view.GetMenuOption(_reports);
        var today = DateTime.Now;

        switch (choice)
        {
            case var _ when choice == _reports[0]:
                var startOfDay = new DateTime(today.Year, today.Month, today.Day);
                HandleReportsInTimeSpan(startOfDay, today, "today");
                break;
            case var _ when choice == _reports[1]:
                var startOfWeek = new DateTime(today.Year, today.Month, today.Day - (int)today.DayOfWeek);
                HandleReportsInTimeSpan(startOfWeek, today, "this week");
                break;
            case var _ when choice == _reports[2]:
                var startOfMonth = new DateTime(today.Year, today.Month, 1);
                HandleReportsInTimeSpan(startOfMonth, today, "this month");
                break;
            case var _ when choice == _reports[3]:
                var startOfYear = new DateTime(today.Year, 1, 1);
                HandleReportsInTimeSpan(startOfYear, today, "this year");
                break;
            case var _ when choice == _reports[4]:
                break;
        }
    }

    private void HandleReportsInTimeSpan(DateTime start, DateTime stop, string timeSpan)
    {
        var sessions =
            service.ReadAllSessionsBetweenDates(start, stop).ToList();
        
        view.DisplaySessions(sessions);

        var totalCodingTime = sessions
            .Aggregate(TimeSpan.Zero, (acc, session) => acc + session.Duration);
        view.DisplayCodingTime(totalCodingTime, timeSpan);
    }

    private void HandleEditSession()
    {
        var sessions = service.ReadAllIncompleteSessions().ToList();

        if (sessions.Count == 0)
        {
            view.DisplayMessage("There are no sessions to edit!");
        }
        else
        {
            view.DisplaySessions(sessions);
            var selection = sessions.ToList().ElementAt(view.GetSessionIndex(sessions));
            
            var stopTime = view.CompleteSession();
            CodingSessionDto editedSession = selection with { StopTime = stopTime };
        
            service.UpdateSession(editedSession);
        }

    }

    private void HandleDeleteSession()
    {
        var sessions = service.ReadAllSessions().ToList();

        if (sessions.Count == 0)
        {
            view.DisplayMessage("There are no sessions to delete!");
        }
        else
        {        
            view.DisplaySessions(sessions);
            var selection = sessions.ToList().ElementAt(view.GetSessionIndex(sessions));
            service.DeleteSession(selection.Id);
        }
    }

    private void HandleExit()
    {
        Running = false;
    }
}