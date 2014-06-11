using System.Collections.Generic;
using System.Linq;

namespace Tests
{
	public static class IEnumerableExtensions
	{
		public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int chunksize)
		{
			while (source.Any())
			{
				yield return source.Take(chunksize);
				source = source.Skip(chunksize);
			}
		}
	}
}
