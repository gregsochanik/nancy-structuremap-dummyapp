using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class Load_tests
	{
		private static readonly string _uriTemplate = "http://localhost/NancyStructureMap3Service/status";

		[Test]
		[Explicit]
		public async void Fire_same_request_at_service()
		{
			var artistIds = Enumerable.Repeat(1, 200);

			const int chunkSize = 100;

			await FireRequests(artistIds, chunkSize, 1000);
		}

		private async static Task FireRequests(IEnumerable<int> artistIds, int concurrent, int delayBetweenBatches)
		{
			var httpClient = new HttpClient();

			foreach (var chunkSet in artistIds.Batch(concurrent))
			{
				var tasks = chunkSet.Select(i =>
				{
					var fullUrl = _uriTemplate;
					return httpClient.GetAsync(fullUrl);
				});

				var whenAll = await Task.WhenAll(tasks);
				foreach (var httpResponseMessage in whenAll.Where(httpResponseMessage => httpResponseMessage.StatusCode != HttpStatusCode.OK))
				{
					Console.WriteLine("{0} -> {1}", httpResponseMessage.StatusCode, httpResponseMessage.RequestMessage.RequestUri);
					var readAsStreamAsync = await httpResponseMessage.Content.ReadAsStringAsync();
					Console.WriteLine(readAsStreamAsync);
				}
				Console.WriteLine("{0} requests fired in current batch", whenAll.Count());
				Console.WriteLine("{0} success", whenAll.Count(httpResponseMessage => httpResponseMessage.StatusCode == HttpStatusCode.OK));
				Console.WriteLine("{0} errored", whenAll.Count(httpResponseMessage => httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError));
				Thread.Sleep(delayBetweenBatches);
			}
		}
	}
}