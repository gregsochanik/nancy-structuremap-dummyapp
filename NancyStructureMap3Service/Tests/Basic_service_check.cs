using System.Net;
using System.Net.Http;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class Basic_service_check
	{
		private const string SERVICE_URI = "http://localhost/NancyStructureMap3Service/status";

		[Test]
		public async void Service_is_returning_OK()
		{
			var httpClient = new HttpClient();
			var response = await httpClient.GetAsync(SERVICE_URI);
			var body = await response.Content.ReadAsStringAsync();

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(body, Is.EqualTo("OK"));
		}


		[Test]
		public async void Service_accepts_a_resource()
		{
			var httpClient = new HttpClient();
			var response = await httpClient.GetAsync(SERVICE_URI + "/1");
			var body = await response.Content.ReadAsStringAsync();

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(body, Is.EqualTo("OK 1"));
		}
	}
}