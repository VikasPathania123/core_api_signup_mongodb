using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Demo.DataAccess.Implementation;
using Demo.Services;

namespace Demo.DataAccess.Factory
{
   public class DataAccessFactory
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
        }
    }
}
