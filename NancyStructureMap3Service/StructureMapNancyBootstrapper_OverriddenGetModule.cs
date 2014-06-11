using System;
using Nancy;
using Nancy.Bootstrappers.StructureMap;
using StructureMap;

namespace NancyStructureMap3Service
{
	public abstract class StructureMapNancyBootstrapper_OverriddenGetModule : StructureMapNancyBootstrapper
	{
		protected override INancyModule GetModule(IContainer container, Type moduleType)
		{
			return (INancyModule)container.GetInstance(moduleType);
		}
	}
}