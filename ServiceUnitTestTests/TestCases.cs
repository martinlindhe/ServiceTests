using System;
using ServiceUnitTest;

public class TestCases
{
	public static void SuccessfulPageLoad ()
	{
		var x = new HttpTester ("http://ga.icn.se/");

		// TODO ASSERT: returns 200 status & at least 300 bytes of data
	}

	public static void RedirectPermanent ()
	{
		// NOTE tests a permanent redirect (HTTP XXXXX number)

		// TODO "should redirect permanently"  http://site.com/  -> http://www.site.com/

		// TODO also check for "temporary redirect". TODO 3: check for ANY redirect code (perm or temp)
	}

	public static void NotFound ()
	{
		// TODO check that request is a 404 and no other error code or failure!
		// "should be 404" http://site.com/not-found
	}

	public static void Restricted ()
	{
		// TODO verify "should be restricted" http://site.com/restricted-part
	}

	public static void JsonResult ()
	{
		// TODO verify url return valid JSON  http://site.com/json-request
	}

	public static void XmlResult ()
	{
		// TODO verify url return valid XML
	}

	public static void SoapResult ()
	{
		// TODO verify url return valid SOAP XML
	}

	public static void GzippedResult ()
	{
		// TODO verify url return gzipped content
	}

	public static void HasCertificate ()
	{
		// TODO verify url has a ssl cert   https://site.com

		// TODO verify cert is VALID
		// TODO verify other details of cert?
	}
}


