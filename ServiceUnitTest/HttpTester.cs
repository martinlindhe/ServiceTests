using System;
using System.Net;
using System.IO;

// TODO: need a "is_url" method (core_dev). extend string class for this
// TODO make user agent changeable
namespace ServiceUnitTest
{
	public class HttpTester
	{
		public static void Main ()
		{
			Console.WriteLine ("wowo");

			var xx = HttpTester.FetchContentType ("http://cellfab.test/");
			Console.WriteLine (xx);
		}

		private static HttpWebResponse PerformFetch (string url)
		{
	
			var request = (HttpWebRequest)WebRequest.Create (url);

			// disable auto redirect
			request.AllowAutoRedirect = false;

			// set timeout to 10 seconds
			request.Timeout = 100000;



			// send client headers

			// IE 6:
			request.UserAgent = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";


			WebResponse response = request.GetResponse ();
			return (HttpWebResponse)response;
		
		}

		/**
		 * @return HTTP status code for URL
		 */
		public static HttpStatusCode FetchStatusCode (string url)
		{
			try {
				var response = PerformFetch (url);

				// print response headers:
				Console.WriteLine (response.Headers);

				var res = response.StatusCode;
				response.Close ();

				return res;
			} catch (WebException ex) {
				// HACK: for some reason, .NET throws an exception on 404 and 500 (maybe more)
			
				HttpWebResponse webResponse = (HttpWebResponse)ex.Response;
				var res = webResponse.StatusCode;

				return res;
			}
		}

		public static string FetchContentType (string url)
		{
			var response = PerformFetch (url);

			return response.ContentType;
		}

		public static byte[] FetchContent (string url)
		{
			var response = PerformFetch (url);

			byte[] res = new byte[0];

			using (BinaryReader reader = new BinaryReader (response.GetResponseStream ())) {
				byte[] scratch = null;

				while ((scratch = reader.ReadBytes (4096)).Length > 0) {
					if (res.Length == 0)
						res = scratch;
					else {
						byte[] temp = new byte[res.Length + scratch.Length];
						Array.Copy (res, temp, res.Length);
						Array.Copy (scratch, 0, temp, res.Length, scratch.Length);
						res = temp;
					}
				}
			}

			return res;
		}
	}
}

