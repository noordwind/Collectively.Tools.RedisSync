using System;
using System.IO;
using Autofac;
using Collectively.Common.Mongo;
using Collectively.Services.Storage.Models.Remarks;
using Collectively.Tools.RedisSync.Framework;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;

namespace Collectively.Tools.RedisSync
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                using (var scope = ConfigureServices().BeginLifetimeScope())
                {
                    var databaseInitializer = scope.Resolve<IDatabaseInitializer>();
                    databaseInitializer.InitializeAsync().Wait();
                    var synchronizer = scope.Resolve<ISynchronizer>();
                    Console.WriteLine("Synchronizing Redis has started.");
                    synchronizer.SynchronizeAsync().Wait();
                    Console.WriteLine("Synchronizing Redis has completed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error while synchronizing Redis.");
                Console.WriteLine(ex);
            }
        }
        static IContainer ConfigureServices()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .SetBasePath(Directory.GetCurrentDirectory());
            var configuration = builder.Build();

            return IoC.GetContainer(configuration);
        }
    }
}
