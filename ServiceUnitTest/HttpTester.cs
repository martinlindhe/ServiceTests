using System;
using System.Net;

// TODO mono dont have System.Net.WebClient ?
// TODO: need a "is_url" method (core_dev). extend string class for this
namespace ServiceUnitTest
{
	public class HttpTester
	{
		public static void Main ()
		{
			Console.WriteLine ("wowo");

			FetchHttpStatusCode ("http://www.google.com/");
		}

		/**
		 * Fetches url and parses HTTP header for HTTP status code
		 */
		public static void FetchHttpStatusCode (string url)
		{
			// TODO is_url url

			using (WebClient client = new System.Net.WebClient ()) {
				// simulate IE 6
				client.Headers ["User-Agent"] =
					"Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
				"(compatible; MSIE 6.0; Windows NT 5.1; " +
				".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

				// print out client headers to be sent to server:
				foreach (var x in client.Headers) {
					Console.WriteLine ("client header " + x);
				}
					
				byte[] arr = client.DownloadData (url);

				Console.WriteLine ("got " + arr.Length + " bytes result");

				// print all server headers:
				foreach (var x in client.ResponseHeaders) {
					Console.WriteLine ("response header " + x);
				}

				// Get response header
				string contentType = client.ResponseHeaders ["Content-Type"];
				Console.WriteLine ("contentType = " + contentType);
			}
		}
	}
}

