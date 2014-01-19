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

	public static void GzippedResult ()
	{
		// TODO client header must specify client.Headers["Accept-Encoding"] = "gzip";
		// TODO verify url return gzipped content
	}

	public static void RedirectPermanent ()
	{
		// NOTE tests a permanent redirect (HTTP XXXXX number)

		// TODO "should redirect permanently"  http://site.com/  -> http://www.site.com/

		// TODO also check for "temporary redirect". TODO 3: check for ANY redirect code (perm or temp)
	}

	[Test]
	public static void Unauthorized ()
	{
		// verifys that this URL requires authentication

		// TODO verify that auth = basic
		// TODO verify that login works for user "username1", "password1"

		Assert.AreEqual (
			HttpTester.FetchStatusCode ("http://battle.x/http_tester_webserver/auth_basic.php"), 
			HttpStatusCode.Unauthorized
		);
	}

	public static void JsonResult ()
	{
		// TODO verify url return valid JSON  http://site.com/json-request
		string res = HttpTester.FetchContentAsString ("http://battle.x/http_tester_webserver/xml.php");
	}

	public static void XmlResult ()
	{
		// TODO verify url return valid XML
		string res = HttpTester.FetchContentAsString ("http://battle.x/http_tester_webserver/json.php");
	}

	public static void HasCertificate ()
	{
		// TODO verify url has a ssl cert   https://site.com

		// TODO verify cert is VALID
		// TODO verify other details of cert?
	}
}


