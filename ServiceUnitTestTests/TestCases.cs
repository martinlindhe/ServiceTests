using System;
using System.Net;
using ServiceUnitTest;
using NUnit.Framework;

// TODO: specify a list of user agents to use to perform each test
// TODO: perform each test once for each user agent
[TestFixture]
public class TestCases
{
	[Test]
	public static void StatusOK ()
	{
		// verify that page status is OK (200)
		Assert.AreEqual (
			HttpTester.FetchStatusCode ("http://battle.x/http_tester_webserver/normal.php"), 
			HttpStatusCode.OK
		);
	}

	[Test]
	public static void StatusNotFound ()
	{
		// verify that page status is NOT FOUND (404)
		Assert.AreEqual (
			HttpTester.FetchStatusCode ("http://battle.x/http_tester_webserver/no-such-file"), 
			HttpStatusCode.NotFound
		);
	}

	[Test]
	public static void ContentLoads ()
	{
		// verify that page content is at least 1000 bytes
		Assert.GreaterOrEqual (
			HttpTester.FetchContent ("http://battle.x/http_tester_webserver/normal.php").Length, 
			20
		);
	}

	[Test]
	public static void ContentType ()
	{
		// verify that content type is set correctly
		Assert.AreEqual (
			HttpTester.FetchContentType ("http://battle.x/http_tester_webserver/normal.php"),
			"text/html; charset=UTF-8"
		);
	}

	[Test]
	public static void ContentCharset ()
	{
		// verify that charset is set correctly
		Assert.AreEqual (
			HttpTester.FetchContentCharset ("http://battle.x/http_tester_webserver/normal.php"),
			"UTF-8"
		);
	}

	[Test]
	public static void GzippedResult ()
	{
		// verify that url is configured to return gzipped content
		byte[] raw = HttpTester.FetchContent ("http://battle.x/http_tester_webserver/gzip.php", true);

		// gzip data stream header
		Assert.AreEqual (raw [0], 0x1F);
		Assert.AreEqual (raw [1], 0x8B);
	}

	[Test]
	public static void MovedPermanently01 ()
	{
		// verify that requested resource has been moved (301)
		Assert.AreEqual (
			HttpTester.FetchStatusCode ("http://battle.x/http_tester_webserver/moved_permanent.php"), 
			HttpStatusCode.MovedPermanently
		);
	}

	[Test]
	public static void MovedPermanently02 ()
	{
		// verify that redirect Location is correct
		Assert.AreEqual (
			HttpTester.FetchLocation ("http://battle.x/http_tester_webserver/moved_permanent.php"), 
			"normal.php"
		);
	}

	[Test]
	public static void MovedTemporarily01 ()
	{
		// verify that requested resource has a redirect (302)
		Assert.AreEqual (
			HttpTester.FetchStatusCode ("http://battle.x/http_tester_webserver/moved_temporary.php"), 
			HttpStatusCode.Redirect
		);
	}

	[Test]
	public static void MovedTemporarily02 ()
	{
		// verify that redirect Location is correct
		Assert.AreEqual (
			HttpTester.FetchLocation ("http://battle.x/http_tester_webserver/moved_temporary.php"), 
			"normal.php"
		);
	}

	[Test]
	public static void Unauthorized ()
	{
		// verify that this URL requires authentication (401)

		// TODO verify that auth = basic
		// TODO verify that login works for user "username1", "password1"

		Assert.AreEqual (
			HttpTester.FetchStatusCode ("http://battle.x/http_tester_webserver/auth_basic.php"), 
			HttpStatusCode.Unauthorized
		);
	}

	public static void JsonResult ()
	{
		// TODO verify url return valid JSON
		string res = HttpTester.FetchContentAsString ("http://battle.x/http_tester_webserver/xml.php");
	}

	public static void XmlResult ()
	{
		// TODO verify url return valid XML
		string res = HttpTester.FetchContentAsString ("http://battle.x/http_tester_webserver/json.php");
	}

	[Test]
	public static void Certificate01 ()
	{
		var cert = HttpTester.FetchCertificate ("https://www.facebook.com/");

		// verify SHA1 hash of certificate
		Assert.AreEqual (
			cert.GetCertHashString (),
			"13D0376C2AB2143640A62D08BB71F5E9EF571361"
		);



		// TODO verify that curent date is between these two dates
		//Console.WriteLine ("effective datee " + cert.GetEffectiveDateString ());
		//Console.WriteLine ("expire date " + cert.GetExpirationDateString ());



		// Console.WriteLine ("format " + cert.GetFormat ()); // X509

		//Console.WriteLine ("key algo " + cert.GetKeyAlgorithm ()); // 1.2.840.113549.1.1.1
		//Console.WriteLine ("key algo params " + cert.GetKeyAlgorithmParametersString ());  // 0500


		//Console.WriteLine ("public key " + cert.GetPublicKeyString ());
	}

	[Test]
	public static void Certificate02 ()
	{
		// FIXME this test works in isolation, but combined with Certificate01 before it,it always fails??
		var cert = HttpTester.FetchCertificate ("https://www.facebook.com/");

		// verify the serial number of the cert
		Assert.AreEqual (
			cert.GetSerialNumberString (),
			"3D4295F31ADC61C1B3B1C1853D850AB6"
		);
	}
}


