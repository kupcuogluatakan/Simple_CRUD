using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;

namespace ODMSTerminal
{
    public static class IoC
    {
        public static IContainer LetThereBeIoC()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            return builder.Build();
        }
    }
}