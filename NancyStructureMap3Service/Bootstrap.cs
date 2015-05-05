using Nancy.Bootstrappers.StructureMap;
using StructureMap;
using StructureMap.Graph;

namespace NancyStructureMap3Service
{
	public class Bootstrap : StructureMapNancyBootstrapper
	{
		protected override void ConfigureApplicationContainer(IContainer existingContainer)
		{
			existingContainer.Configure(x => x.Scan(scanner =>
			{
				scanner.TheCallingAssembly();
				scanner.WithDefaultConventions();
				scanner.SingleImplementationsOfInterface();
			}));
		}
	}
}