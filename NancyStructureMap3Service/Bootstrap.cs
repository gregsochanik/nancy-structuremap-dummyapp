using StructureMap;

namespace NancyStructureMap3Service
{
	public class Bootstrap : StructureMapNancyBootstrapper_OverriddenGetModule
	{
		protected override IContainer GetApplicationContainer()
		{
			return ObjectFactory.Container;
		}
	}
}