This project is a coding time tracker written in C#. It is part of my journey to learn C# that I am undertaking next to a computer science degree.

Features
- Create ongoing and past coding sessions manually.
- Stopwatch a coding session, with time shown and automatic session creation on stopping the watch.
- View all coding sessions.
- Get reports with total coding time per defined time spans.
- Update ongoing sessions.
- Delete sessions.
- Specify local database location with configuration file.

Repository layer uses SQLite and Dapper ORM.

What I've learned
- I've tried (again) to build the app using layered architecture principles by layering the code according to their responsibilities, with a Repository layer reading
  and writing the data from the database, a Service layer enforcing business rules and converting the data to lightweight data-only classes (DTOs), a View layer
  for humans to interact with the app and a Controller to orchestrate data exchange between the View and the Service layers. I think that will help me for the
  Web-based projects.
- This time, I've done a full domain object (the CodingSession) that enforces logical and business rules.
- I've learned to integrate third-party dependencies into my app in C#. It is quite painless! Way better than the languages I've been learning in university.
- I don't like how LINQ renames all the higher-order functions (map is Select, filter is Where, fold is Aggregate?!). I wish they'd kept the "standard" names.
