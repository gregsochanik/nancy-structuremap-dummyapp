using Nancy;

namespace NancyStructureMap3Service
{
	public class StatusModule : NancyModule
	{
		public StatusModule()
		{
			Get["/status"] = _ => "OK";
		}
	}
}