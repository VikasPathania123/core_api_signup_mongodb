
using Demo.DataAccess.Factory;

namespace Demo.API.Factory
{
    using Autofac;
    using Demo.Infrastructure.Factory;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ApiFactory
    {
        public static void RegisterDependencies(ContainerBuilder builder, IConfiguration configration)
        {
            InfrastructureFactory.RegisterDependencies(builder);
            DataAccessFactory.RegisterDependencies(builder);
        }
    }
}
