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
		private const string SERVICE_URI = "http://localhost/NancyStructureMap3Service/status/{0}";

		[Test]
		[Explicit]
		public async void Fire_same_request_at_service()
		{
			var requests = Enumerable.Range(1, 1000);

			const int chunkSize = 500;
			const int delayBetweenBatchesMs = 1000;

			await FireRequests(requests, chunkSize, delayBetweenBatchesMs);
		}

		private async static Task FireRequests(IEnumerable<int> ids, int concurrent, int delayBetweenBatches)
		{
			var httpClient = new HttpClient();

			foreach (var chunkedSet in ids.Batch(concurrent))
			{
				var tasks = chunkedSet.Select(id => httpClient.GetAsync(string.Format(SERVICE_URI, id)));

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