using System.Globalization;
using CodingTracker.Application.DTOs;
using CodingTracker.UI.Abstractions;
using Spectre.Console;

namespace CodingTracker.UI.Implementations;

public class ConsoleView : IView
{
    private const string DateFormat = "yyyy/MM/dd HH:mm:ss";
    public void DisplayHeader()
    {
        AnsiConsole.Clear();
        
        AnsiConsole.WriteLine("<><><> CLI Coding Session Tracker <><><><><><><><>");
    }

    public string GetMenuOption(IReadOnlyList<string> options)
    {
        var opts = options.ToArray();
        
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .PageSize(opts.Length)
                .AddChoices(opts)
            );
    }

    public DateTime GetOngoingSessionInfo()
    {
        AnsiConsole.Clear();
        
        AnsiConsole.WriteLine("Enter a date and time for the start of this session.");
        return GetDateFromUser();
    }

    public (DateTime, DateTime) GetCompletedSessionInfo()
    {
        AnsiConsole.Clear();
        
        AnsiConsole.WriteLine("Enter a date and time for the start of this session.");
        var start = GetDateFromUser();

        AnsiConsole.WriteLine("Enter a date and time for the end of this session.");
        var stop = GetDateFromUser();

        return (start, stop);
    }

    public int GetSessionIndex(IReadOnlyList<CodingSessionDto> sessions)
    {
        var count = sessions.ToList().Count;
        var entry = AnsiConsole.Prompt(
            new TextPrompt<int>("Enter the selected session:")
                .Validate(n =>
                    {
                        if (n <= 0 || n > count)
                        {
                            return ValidationResult.Error($"[red]Selection out of bounds (1..{count}).[/]");
                        }

                        return ValidationResult.Success();
                    })
            );
         return entry - 1;
    }

    public DateTime CompleteSession()
    {
        AnsiConsole.WriteLine("Enter a date and time for the end of this session.");
        var stop = GetDateFromUser();

        return stop;
    }

    public void DisplayStopwatchTime(TimeSpan time)
    {
        AnsiConsole.Cursor.SetPosition(0, 1);
        AnsiConsole.Cursor.Show(false);
        AnsiConsole.WriteLine(time.ToString());
        AnsiConsole.WriteLine("Press a key to stop the stopwatch.");
        
    }

    public bool StopStopwatch()
    {
        AnsiConsole.Clear();
        while (!Console.KeyAvailable)
        {
            Thread.Sleep(100);
        }
        _ = Console.ReadKey();
        AnsiConsole.Cursor.SetPosition(0, 3);
        AnsiConsole.WriteLine("Session completed!");
        return true;
    }

    public void DisplaySessions(IReadOnlyList<CodingSessionDto> sessions)
    {
        AnsiConsole.Clear();
        
        int c = 0;
        foreach (var session in sessions)
        {
            AnsiConsole.WriteLine($"Coding Session {++c}");
            AnsiConsole.WriteLine(session.ToString());
        }
    }

    public void DisplaySession(CodingSessionDto session)
    {
        AnsiConsole.Clear();
        
        AnsiConsole.WriteLine(session + "\n");
    }

    public void DisplayCodingTime(TimeSpan time, string timeSpan)
    {
        AnsiConsole.WriteLine($"You have coded for {time} {timeSpan}.");
    }

    public void DisplayMessage(string message)
    {
        AnsiConsole.WriteLine(message);
    }

    private DateTime GetDateFromUser()
    {
        var entry =
            AnsiConsole.Prompt(
                new TextPrompt<string>($"({DateFormat}):")
                    .Validate(entry =>
                        {
                            if (IsDateCorrectFormat(entry, DateFormat))
                            {
                                return ValidationResult.Success();
                            }

                            return ValidationResult.Error($"[red]Make sure format is {DateFormat}.[/]");
                        }
                    )
            );
        return DateTime.ParseExact(entry, DateFormat, CultureInfo.InvariantCulture);
    }

    private bool IsDateCorrectFormat(string date, string format)
    {
        return DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}