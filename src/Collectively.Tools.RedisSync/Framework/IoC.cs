using Autofac;
using Collectively.Common.Caching;
using Collectively.Common.Extensions;
using Collectively.Common.Mongo;
using Microsoft.Extensions.Configuration;

namespace Collectively.Tools.RedisSync.Framework
{
    public static class IoC
    {
        public static IContainer GetContainer(IConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<MongoDbModule>();
            builder.RegisterModule<RedisModule>();
            builder.RegisterType<MongoDbInitializer>().As<IDatabaseInitializer>();
            builder.RegisterType<Synchronizer>().As<ISynchronizer>();
            builder.RegisterInstance(configuration.GetSettings<MongoDbSettings>()).SingleInstance();
            builder.RegisterInstance(configuration.GetSettings<RedisSettings>()).SingleInstance();

            return builder.Build();
        }        
    }
}