using ODMSCommon.Logging;

namespace ODMSTerminal.Infrastructure.Modules
{
    using Autofac;
    using ODMSBusiness;
    using ODMSBusiness.Terminal.Common;
    using ODMSData.Utility;

    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var assemblies = new[] { ThisAssembly, typeof(IMediator).Assembly };

            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(IHandleRequest<,>))
                .InstancePerRequest();  

            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(IHandleCommand<>))
                .InstancePerRequest();

            builder.RegisterType<AutofacMediator>()
                .As<IMediator>()
                .InstancePerRequest();

            builder.RegisterType<DbHelper>()
                .As<DbHelper>()
                .InstancePerRequest();

            builder.RegisterType<DeliveryGoodsPlacementBL>()
                .As<DeliveryGoodsPlacementBL>()
                .InstancePerRequest();

            builder.RegisterType<WarehouseBL>()
              .As<WarehouseBL>()
              .InstancePerRequest();

            builder.RegisterType<StockRackDetailBL>()
             .As<StockRackDetailBL>()
             .InstancePerRequest();

            builder.RegisterType<WorkOrderPickingDetailBL>().AsSelf().InstancePerRequest();
            builder.RegisterType<Loggable>().AsSelf().InstancePerRequest();


        }
    }
}