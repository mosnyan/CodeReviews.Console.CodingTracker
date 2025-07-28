using System.Globalization;
using CodingTracker.Application.DTOs;
using CodingTracker.UI.Abstractions;
using Spectre.Console;

namespace CodingTracker.UI.Implementations;

public class ConsoleView : IView
{
    private const string DateFormat = "yyyy/MM/dd hh:mm:ss";
    public void DisplayHeader()
    {
        AnsiConsole.WriteLine("CLI Coding Session Tracker App");
    }

    public void DisplayMainMenuOptions(IEnumerable<string> options)
    {
        throw new NotImplementedException();
    }

    public DateTime GetOngoingSessionInfo()
    {
        AnsiConsole.WriteLine("Enter a date and time for the start of this session.");
        return GetDateFromUser();
    }

    public (DateTime, DateTime) GetCompletedSessionInfo()
    {
        throw new NotImplementedException();
    }

    public DateTime GetSessionCompletionTime()
    {
        throw new NotImplementedException();
    }

    public void DisplaySessions(IEnumerable<CodingSessionDto> habits)
    {
        throw new NotImplementedException();
    }

    public void DisplaySession(CodingSessionDto session)
    {
        throw new NotImplementedException();
    }

    public int GetUserChoice()
    {
        throw new NotImplementedException();
    }

    public void DisplayMessage(string message)
    {
        throw new NotImplementedException();
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