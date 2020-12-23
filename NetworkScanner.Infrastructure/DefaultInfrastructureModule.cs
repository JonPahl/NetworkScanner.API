using Autofac;
using NetworkScanner.Infrastructure.Data;
using NetworkScanner.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Reflection;
using Module = Autofac.Module;

namespace NetworkScanner.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        protected bool IsDevelopment { get; }
        protected List<Assembly> Assemblies = new List<Assembly>();

        public DefaultInfrastructureModule(bool isDevelopment = false, Assembly callingAssembly = null)
        {
            IsDevelopment = isDevelopment;
            var infrastructureAssembly = Assembly.GetAssembly(typeof(EfRepository));
            Assemblies.Add(infrastructureAssembly);
            if (callingAssembly != null)
                Assemblies.Add(callingAssembly);
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (IsDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }

            RegisterCommonDependencies(builder);
        }

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<EfRepository>().As<IRepository>()
                .InstancePerLifetimeScope();
        }

        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder = null)
        {
            if (builder != null)
            {
                // TODO: Add development only services
            }
        }

        private void RegisterProductionOnlyDependencies(ContainerBuilder builder = null)
        {
            if (builder != null)
            {
                // TODO: Add production only services
            }
        }
    }
}
