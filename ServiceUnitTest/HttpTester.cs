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

			var xx = (int)HttpTester.FetchStatusCode ("http://cellfab.test/no-such-file");
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
			
				HttpWebResponse webResponse = (HttpWebResponse)ex.Response;
				var res = webResponse.StatusCode;

				return res;
			}
		}

		public static string FetchContent (string url)
		{
			var response = PerformFetch (url);

			// Console.WriteLine ("Content type is {0}", response.ContentType);


			// Get the stream associated with the response.
			Stream receiveStream = response.GetResponseStream ();

			// Pipes the stream to a higher level stream reader with the required encoding format. 
			StreamReader readStream = new StreamReader (receiveStream, System.Text.Encoding.UTF8);

			Console.WriteLine ("Response stream received.");
			string res = readStream.ReadToEnd ();
			//Console.WriteLine (res.Length);
			response.Close ();
			readStream.Close ();

			return res;
		}
	}
}

