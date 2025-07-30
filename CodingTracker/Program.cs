// See https://aka.ms/new-console-template for more information

using CodingTracker.Application.Abstractions.Repositories;
using CodingTracker.Application.Services;
using CodingTracker.Infrastructure.Persistence;
using CodingTracker.Infrastructure.Repositories;
using CodingTracker.UI.Abstractions;
using CodingTracker.UI.Controllers;
using CodingTracker.UI.Implementations;

const string connectionString = "Data Source = sessiondb";
Initializer initializer = new Initializer(connectionString);
ISessionRepository repository = new SessionRepository(connectionString);

SessionService service = new SessionService(repository);

IView view = new ConsoleView();
ConsoleUiController controller = new ConsoleUiController(service, view);

initializer.Initialize();
controller.Initialize();
while (controller.Running)
{
    controller.Execute();
}