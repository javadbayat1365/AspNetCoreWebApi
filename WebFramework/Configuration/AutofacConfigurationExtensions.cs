
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using Common.AutofacInterfaceAsMark;
using Data;
using Data.Contracts;
using Data.Repositories;
using Entities.Common;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.ControlServices;

namespace WebFramework.Configuration
{
    public static class AutofacConfigurationExtensions
    {
        public static void AddServices(this ContainerBuilder containerBuilder)
        {
            //RegisterType > As > Liftetime
            containerBuilder.RegisterGeneric(typeof(GenericRepo<>)).As(typeof(IGenericRepo<>)).InstancePerLifetimeScope();

           // containerBuilder.RegisterType<UserServices>().As<IUserServices>().InstancePerDependency(); //transient

            var commonAssembly = typeof(SiteSettings).Assembly;
            var entitiesAssembly = typeof(IEntity).Assembly;
            var dataAssembly = typeof(AppDBContext).Assembly;
            var servicesAssembly = typeof(JwtService).Assembly;

            var assemblies = System.Reflection.Assembly.GetExecutingAssembly();

            containerBuilder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //transient
            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly)
                .AssignableTo<ISingletonDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        public static IServiceProvider BuildAutofacServiceProvider(this IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            //Register Services to Autofac ContainerBuilder
            containerBuilder.AddServices();

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
