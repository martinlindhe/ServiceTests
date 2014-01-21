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

			var xx = HttpTester.FetchAuthType ("http://battle.x/http_tester_webserver/auth_basic.php");
			VarDump.Pretty (xx);





			/*
			var cert1 = HttpTester.FetchCertificate ("https://www.facebook.com/");
			// verify SHA1 hash of certificate
			Console.WriteLine (
				cert1.GetCertHashString ()
			);

			var cert2 = HttpTester.FetchCertificate ("https://www.facebook.com/");
			// verify the serial number of the cert
			Console.WriteLine (
				cert2.GetSerialNumberString () // XXX always fails!
			);
			*/
		}

		public static X509Certificate FetchCertificate (string url)
		{
			if (!url.IsUrl ())
				throw new FormatException ("not a URL");
				
			Uri u = new Uri (url);
			ServicePoint sp = ServicePointManager.FindServicePoint (u);
			sp.MaxIdleTime = 360000;

			string groupName = Guid.NewGuid ().ToString ();

			var request = (HttpWebRequest)WebRequest.Create (u);
			request.ConnectionGroupName = groupName;

			request.Timeout = 100000; // 10 sec
			request.AllowAutoRedirect = false;
			request.UserAgent = ua_IE6;

			using (WebResponse response = request.GetResponse ()) {
				// Ignore response, and close the response.
			}

			sp.CloseConnectionGroup (groupName);

			// FIXME on 2nd request, i dont get the cert for https://www.facebook.com
			if (sp.Certificate == null)
				throw new Exception ("no certificate found");

			return sp.Certificate;
		}

		/**
		 * @return HTTP status code for URL
		 */
		public static HttpStatusCode FetchStatusCode (string url)
		{
			var response = PerformFetch (url);

			// print response headers
			// Console.WriteLine (response.Headers);

			var res = response.StatusCode;
			response.Close ();

			return res;
		}

		/**
		 * Parses auth type from WWW-Authenticate response header
		 */
		public static string FetchAuthType (string url)
		{
			var response = PerformFetch (url);

			string auth = response.GetResponseHeader ("WWW-Authenticate"); // Basic realm="my realm"
		
			// explode at space, return first token
			string[] xx = auth.Split (' ');
			return xx [0];

			// TODO return ENUM of auth type: Basic, XXX XXX
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

			string contentType = response.ContentType; // text/html; charset=UTF-8

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
			try {
				var u = new Uri (url);
				var request = (HttpWebRequest)WebRequest.Create (u);

				request.Timeout = 100000; // 10 sec
				request.AllowAutoRedirect = false;
				request.UserAgent = ua_IE6;

				if (gzipped)
					request.Headers.Add (HttpRequestHeader.AcceptEncoding, "gzip");

				return (HttpWebResponse)request.GetResponse ();
	
			} catch (WebException ex) {
				// .NET throws an exception on 401, 404 and 500 (and more)

				return (HttpWebResponse)ex.Response;
			}
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

