# Automation Toolkit 3.0

This project comes to replace the Automation toolkit app (internal company code generator tool).

## Features

Generate copies of stored template folder structures and replacing file keywords.
Create new template presets.
Auto generate Windows forms based on designated data source. (*Not yet supported in Maui client*)

## Tech

### Server:
- .NET 6.0 WebApi standalone app

### Client:
- Electron app, using Angular (Can spawn server process automatically)
- .NET MAUI using Blazor (Server must be run externally)

## Development setup

### Server

Run server:

Install [.Net 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

```shell
$ cd server/AutomationToolkit.Api && dotnet run
```

### Client - Angular 
- Install [Node Version Manager](https://github.com/coreybutler/nvm-windows) and then install NodeJs 8:

```shell
$ nvm install 8.0.0
```

- Install dependencies:

```shell
$ cd client/Angular && npm install
```

Run client dev server:

```shell
$ cd client/Angular && npm start
```

Open a web browser to http://localhost:4200

### Client - MAUI

Requires Visual Studio 2022 (Preview) >17.3.0 with Workload: .NET Multi-platform app UI development

Open project /client/Maui/AutomationToolkit.Client.Blazor.csproj

## Deployment

### Angular
```shell
$ cd client/Angular && npm run full-publish
```
> This will generate the folder 'Automation Toolkit 2-win32-x64'

### MAUI (Coming Soon)
https://docs.microsoft.com/en-us/dotnet/maui/windows/deployment/overview