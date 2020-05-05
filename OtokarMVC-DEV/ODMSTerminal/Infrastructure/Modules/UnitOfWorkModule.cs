using ODMSBusiness.Terminal.Common;

namespace ODMSTerminal.Infrastructure.Modules
{
    using Autofac;
    public class UnitOfWorkModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();
        }
    }
}