![Collectively](https://github.com/noordwind/Collectively/blob/master/assets/collectively_logo.png)

----------------

|Branch             |Build status                                                  
|-------------------|-----------------------------------------------------
|master             |[![master branch build status](https://api.travis-ci.org/noordwind/Collectively.Tools.RedisSync.svg?branch=master)](https://travis-ci.org/noordwind/Collectively.Tools.RedisSync)
|develop            |[![develop branch build status](https://api.travis-ci.org/noordwind/Collectively.Tools.RedisSync.svg?branch=develop)](https://travis-ci.org/noordwind/Collectively.Tools.RedisSync/branches)

**Let's go for the better, Collectively​​.**
----------------

**Collectively** is an open platform to enhance communication between counties and its residents​. It's made as a fully open source & cross-platform solution by [Noordwind](https://noordwind.com).

Find out more at [becollective.ly](http://becollective.ly)

**Collectively.Tools.RedisSync**
----------------

The **Collectively.Tools.RedisSync** is a console application which synchronizes the Redis cache using Storage Service database.


**Quick start**
----------------

In order to run the **Collectively.Tools.RedisSync** you need to have installed:
- [.NET Core](https://dotnet.github.io)

Clone the repository and start the application using the following commands:

```
git clone https://github.com/noordwind/Collectively.Tools.RedisSync
cd src/Collectively.Tools.RedisSync
dotnet restore --source https://api.nuget.org/v3/index.json --source https://www.myget.org/F/collectively/api/v3/index.json --no-cache
dotnet run --no-restore
```

Once executed, you should see the Redis cache filled with the data.


**Configuration**
----------------

Please edit the *appsettings.json* file in order to use the custom application settings.

**Tech stack**
----------------
- **[.NET Core](https://dotnet.github.io)** - an open source & cross-platform framework for building applications using C# language.service bus.

**Solution structure**
----------------
- **Collectively.Tools.RedisSync** - core and executable project via *dotnet run --no-restore* command.