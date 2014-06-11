using Nancy;

namespace NancyStructureMap3Service
{
	public class StatusModule : NancyModule
	{
		public StatusModule()
		{
			Get["/status"] = _ => "OK";
			Get["/status/{id}"] = _ => "OK " + _.id;
		}
	}
}