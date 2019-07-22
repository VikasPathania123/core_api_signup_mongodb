namespace Demo.Infrastructure.Factory
{
    using Autofac;
    using Demo.Infrastructure.Implementation;

    public class InfrastructureFactory
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<MongoDbStoreSettings>().As<IMongoDbStoreSettings>();
            builder.RegisterType<AppSettings>().As<IAppSettings>();
        }

    }
}
