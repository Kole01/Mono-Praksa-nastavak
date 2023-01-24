using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Test.Repository.Common;
using Test.Repository;
using Test.Service.Common;
using Test.Service;
using Test.Model;

namespace Test.WebApl.App_Start
{
    public class AutofacConfig : Autofac.Module
    {
         public static void Load()
        {
            //var config = GlobalConfiguration.Configuration;
            //var builder = new ContainerBuilder();
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterType<CustomerService>().As<ICustomerService>();
            //builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();

            //var container = builder.Build();
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);


            

            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new CustomerServiceModule()) ;
            builder.RegisterModule(new CustomerRepositoryModule());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            


        }
    }
}