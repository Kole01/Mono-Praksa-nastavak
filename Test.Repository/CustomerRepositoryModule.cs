using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Test.Repository.Common;

namespace Test.Repository
{
    public class CustomerRepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder  builder)
        {
            builder.RegisterType(typeof(CustomerRepository)).As(typeof(ICustomerRepository));
        }
    }
}
