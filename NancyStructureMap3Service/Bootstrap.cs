using StructureMap;
using StructureMap.Graph;

namespace NancyStructureMap3Service
{
	public class Bootstrap : StructureMapNancyBootstrapper_OverriddenGetModule
	{
		protected override IContainer GetApplicationContainer()
		{
			var container = new Container(x => x.Scan(scanner =>
			{
				scanner.TheCallingAssembly();
				scanner.LookForRegistries();
				scanner.WithDefaultConventions();
			}));

			return container;
		}
	}
}