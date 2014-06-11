using System;
using System.Diagnostics;
using System.Web;
using StructureMap;
using StructureMap.Graph;

namespace NancyStructureMap3Service
{
	public class Global : HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			ObjectFactory.Initialize(x => x.Scan(scanner =>
			{
				scanner.TheCallingAssembly();
				scanner.LookForRegistries();
				scanner.WithDefaultConventions();
			}));
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			new GlobalExceptionHandler(new HttpContextWrapper(Context)).Handle();
		}

		public class GlobalExceptionHandler
		{
			private readonly HttpContextBase _context;

			public GlobalExceptionHandler(HttpContextBase context)
			{
				_context = context;
			}

			public void Handle()
			{
				var exception = _context.Server.GetLastError();
				Debug.WriteLine(exception);
			}
		}
	}
}