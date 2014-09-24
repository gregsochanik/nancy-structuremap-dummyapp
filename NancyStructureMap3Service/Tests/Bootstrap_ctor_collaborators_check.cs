using System.Net;
using System.Net.Http;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class Bootstrap_ctor_collaborators_check
	{
		private const string TEST_SERVICE_URI = "http://localhost/NancyStructureMap3Service/test";

		[Test]
		public async void Service_can_resolve_concrete_from_interface()
		{
			var httpClient = new HttpClient();
			var response = await httpClient.GetAsync(TEST_SERVICE_URI);
			var body = await response.Content.ReadAsStringAsync();

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(body, Is.EqualTo("Hello world!!"));
		}
	}
}