using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Test.Service;
using Test.Service.Common;
using Test.Repository;
using Test.Repository.Common;

namespace Test.Service
{
    public class CustomerServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<CustomerService>();

            builder.RegisterType(typeof(CustomerService))
            .As(typeof(ICustomerService));

        }        

    }
}
