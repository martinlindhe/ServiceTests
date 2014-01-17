using System;
using System.Net;

// TODO: need a "is_url" method (core_dev). extend string class for this
namespace ServiceUnitTest
{
	public class HttpTester
	{
		public static void Main ()
		{
			Console.WriteLine ("wowo");

			var xx = (int)FetchStatusCode ("http://www.dn.se/");
			Console.WriteLine (xx);
		}

		public static HttpStatusCode FetchStatusCode (string url)
		{
			var request = (HttpWebRequest)WebRequest.Create (url);

			// disable auto redirect since it will break our use case
			request.AllowAutoRedirect = false;

			// set timeout to 10 seconds
			request.Timeout = 100000;



			// send client headers

			// IE 6:
			request.UserAgent = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

		
			using (WebResponse response = request.GetResponse ()) {
				HttpWebResponse httpResponse = (HttpWebResponse)response;

				// print response headers:
				Console.WriteLine (request.GetResponse ().Headers);

				return httpResponse.StatusCode;
			}
		}
	}
}

