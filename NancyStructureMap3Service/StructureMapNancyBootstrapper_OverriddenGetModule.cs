using System;
using Nancy;
using Nancy.Bootstrappers.StructureMap;
using StructureMap;

namespace NancyStructureMap3Service
{
	public abstract class StructureMapNancyBootstrapper_OverriddenGetModule : StructureMapNancyBootstrapper
	{
		/// <summary>
		/// Retreive a specific module instance from the container
		/// </summary>
		/// <param name="container">Container to use</param>
		/// <param name="moduleType">Type of the module</param>
		/// <returns>A <see cref="INancyModule"/> instance</returns>
		protected override INancyModule GetModule(IContainer container, Type moduleType)
		{
			return (INancyModule)container.GetInstance(moduleType);
		}
	}
}