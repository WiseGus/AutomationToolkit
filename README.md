# Automation Toolkit 2.1

This project comes to replace the Automation toolkit app (internal company code generator tool).

## Features

Generate copies of stored template folder structures and replacing file keywords.
Create new template presets.
Auto generate Windows forms based on designated data source.

## Tech

Created as an Electron app, using Angular for frontend and ASP.Net Core as backend.
All wrapped in one executable file.

## Development setup

- Install [Node Version Manager](https://github.com/coreybutler/nvm-windows) and then install NodeJs 8:

```shell
$ nvm install 8.0.0
```

- Install [.Net 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

- Install client dependencies:

```shell
$ cd client && npm install
```

- Install server dependencies:

```shell
$ cd server && dotnet restore
```

Run server:

```shell
$ cd server && dotnet run
```

Run client dev server:

```shell
$ cd client && npm start
```

Open a web browser to http://localhost:4200

## Deployment
```shell
$ cd client && npm run full-publish
```
> This will generate the folder 'Automation Toolkit 2-win32-x64'