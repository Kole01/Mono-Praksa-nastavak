using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Test.Service.Common;
using Test.Service;
using System.Diagnostics;
using Test.Repository;
using Test.Repository.Common;
using System.Reflection;
using Autofac.Integration.WebApi;
using Test.WebApl.App_Start;

namespace Test.WebApl
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AutofacConfig.Load();
            GlobalConfiguration.Configure(WebApiConfig.Register);

        }


    }
}
