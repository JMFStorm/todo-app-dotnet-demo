# TodoAppDemo

Created using Windows 10 as development environment.

## Technologies

[ASP.NET Core 8.0](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0) - Backend server for HTTP API
  
[Entity Framework Core 8](https://learn.microsoft.com/en-us/ef/core/) - For managing database connection
  
**[xUnit.net](https://xunit.net/)** - Unit testing. Used in `TodoDemoAppTests/TodoDemoAppTests.csproj`
    
**[Vue.js 3](https://vuejs.org/)** - Web browser UI. Found under `TodoAppDemo/wwwroot/index.html`

## Scripts

- `run_dev_server.bat`: Starts project locally `TodoDemoApp/TodoDemoApp.csproj` on localhost:5000

- `run_unit_tests.bat`: Runs Xunit tests on `TodoDemoAppTests/TodoDemoAppTests.csproj`

- `publish_project_win-x64.bat`: Builds a release of `TodoDemoApp/TodoDemoApp.csproj` for Windows

