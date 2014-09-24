using Nancy;

namespace NancyStructureMap3Service
{
	public class StatusModule : NancyModule
	{
		private readonly ITestRepo _testRepo;
		
		public StatusModule(ITestRepo testRepo)
		{
			_testRepo = testRepo;

			Get["/test"] = _ => _testRepo.GetSomeThing();
			Get["/status"] = _ => "OK";
			Get["/status/{id}"] = _ => "OK " + _.id;
		}
	}

	public interface ITestRepo
	{
		string GetSomeThing();
	}

	public class TestRepo : ITestRepo
	{
		public string GetSomeThing()
		{
			return "Hello world!!";
		}
	}

}