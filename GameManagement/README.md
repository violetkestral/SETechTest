# Game Management

## Purpose
> A web application that displays a list of games and related platforms that allows a user to add, edit and delete entries from the list using .NET (C#) and any JavaScript Front End Framework (Preferably AngularJS or 2+).

## How to run

Pull down the code and open the <a href="https://github.com/violetkestral/SETechTest/blob/master/GameManagement/GameManagement.sln">solution</a>

The application can be run either in IIS or in Docker containers, though the functionality remains the same

### IIS

1) Ensure the 'GameManagement' project is set as the startup project
2) Select 'IIS Express' as the run option
3) Run!

### Docker
**Pre-Requesites**
- Docker Desktop installed with linux containers

1) Ensure the 'docker-compose' project is set as the startup project
2) Run!

## Technologies Used
- C# Asp.NET Core
- AngularJS
- <a href="https://github.com/jbogard/MediatR">MediatR</a>
- EnityFramework Core
- SqlServer (localdb)
- Docker
- XUnit
- AutoFixture
