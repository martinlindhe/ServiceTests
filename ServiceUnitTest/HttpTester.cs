using System;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using Punku;

// TODO make user agent changeable
namespace ServiceUnitTest
{
	public class HttpTester
	{
		static string ua_IE6 = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
		static string ua_FF26_Mac = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.9; rv:26.0) Gecko/20100101 Firefox/26.0";
		static string ua_Chrome32_Mac = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.77 Safari/537.36";

		public static void Main ()
		{
			Console.WriteLine ("wowo");

			var xx = HttpTester.FetchLocation ("http://battle.x/http_tester_webserver/moved_permanent.php");

			VarDump.Pretty (xx);
		}

		public static X509Certificate FetchCertificate (string url)
		{
			if (!url.IsUrl ())
				throw new FormatException ("not a URL");

			var request = (HttpWebRequest)WebRequest.Create (url);

			request.Timeout = 100000; // 10 sec
			request.AllowAutoRedirect = false;
			request.UserAgent = ua_IE6;

			WebResponse response = request.GetResponse ();

			if (request.ServicePoint.Certificate == null)
				throw new Exception ("no certificate found");

			return request.ServicePoint.Certificate;
		}

		/**
		 * @return HTTP status code for URL
		 */
		public static HttpStatusCode FetchStatusCode (string url)
		{
			try {
				var response = PerformFetch (url);

				// print response headers
				// Console.WriteLine (response.Headers);

				var res = response.StatusCode;
				response.Close ();

				return res;
			} catch (WebException ex) {
				// HACK for some reason, .NET throws an exception on 404 and 500 (maybe more)
			
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

		/**
		 * Returns value of Location response header
		 */
		public static string FetchLocation (string url)
		{
			var response = PerformFetch (url);

			return response.GetResponseHeader ("Location");
		}

		/**
		 * Extracts document encoding from HTTP header "Content-Type"
		 */
		public static string FetchContentCharset (string url)
		{
			var response = PerformFetch (url);

			// "text/html; charset=UTF-8"
			string contentType = response.ContentType;

			if (!contentType.Contains ("charset="))
				throw new Exception ("Content-Type does not contain charset");

			// TODO improve parsing
			string[] lines = Regex.Split (contentType, "charset=");
			return lines [1];
		}

		public static byte[] FetchContent (string url, bool gzipped = false)
		{
			var response = PerformFetch (url, gzipped);

			return ReadResponseStream (response.GetResponseStream ());
		}

		public static string FetchContentAsString (string url)
		{
			var response = PerformFetch (url);

			byte[] bytes = ReadResponseStream (response.GetResponseStream ());

			// TODO see HTTP headers for text encoding
			// TODO handle non-utf8 encodings + add some examples, latin1->utf8. 

			return Encoding.UTF8.GetString (bytes);
		}

		private static HttpWebResponse PerformFetch (string url, bool gzipped = false)
		{
			if (!url.IsUrl ())
				throw new FormatException ("not a URL");

			var request = (HttpWebRequest)WebRequest.Create (url);

			request.Timeout = 100000; // 10 sec
			request.AllowAutoRedirect = false;
			request.UserAgent = ua_IE6;

			if (gzipped) {
				request.Headers.Add (HttpRequestHeader.AcceptEncoding, "gzip");
			}

			WebResponse response = request.GetResponse ();
			return (HttpWebResponse)response;
		}

		private static byte[] ReadResponseStream (Stream stream)
		{
			byte[] res = new byte[0];

			using (var reader = new System.IO.BinaryReader (stream)) {
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

