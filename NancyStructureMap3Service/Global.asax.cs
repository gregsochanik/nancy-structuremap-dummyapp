using System;
using System.Diagnostics;
using System.Web;

namespace NancyStructureMap3Service
{
	public class Global : HttpApplication
	{
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