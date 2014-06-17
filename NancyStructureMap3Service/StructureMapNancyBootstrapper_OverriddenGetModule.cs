using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using Nancy.Routing;
using Nancy.ViewEngines;
using StructureMap;

namespace NancyStructureMap3Service
{
	public abstract class StructureMapNancyBootstrapper_OverriddenGetModule : StructureMapNancyBootstrapper
	{
		protected override INancyModule GetModule(IContainer container, Type moduleType)
		{
			return (INancyModule)container.GetInstance(moduleType);
		}

		protected override System.Collections.Generic.IEnumerable<IRequestStartup> RegisterAndGetRequestStartupTasks(IContainer container, Type[] requestStartupTypes)
		{
			return container.GetAllInstances<IRequestStartup>();
		}

		protected override void RegisterBootstrapperTypes(IContainer applicationContainer)
		{
			applicationContainer.Configure(registry =>
			{
				registry.For<INancyModuleCatalog>().Singleton().Use(this);
				registry.For<IFileSystemReader>().Singleton().Use<DefaultFileSystemReader>();
			});
		}
	}
}