== About ==

HTTP server "black box" unit test suite for .NET
"Unit Test" a deployed system by describing "known" operation
as tests.

Example use cases:
* verify that your website stays configured correctly to handle permanent redirects
* verify that the served SSL certificate is not changed
* verify that JSON api is working as expected
* test that current DEV/LIVE/EXTERNAL system configuration assumptions stays true

The tests should work for any HTTP/HTTPS web server.


    
Idea from http://robb.weblaws.org/2014/01/16/new-open-source-library-for-test-driven-devops/
Something similar, for Ruby: https://github.com/hannestyden/hyperspec


== TODO core ==
    * can we extend NUnit ?
    * merge the tiny http test webpage source. currently lives in fmf repository under http_tester_webserver

== TODO HTTP API testers ==
    * XML parser: verify result is strict/loose formatted xml
    * SOAP API tester: verify functions exists etc? verify WSDL is in place?
    * JSON parser: verify result is as expected

== TODO new modules ==
    MySQL tester
	SMTP tester, for gmail
	SSH tester
	FTP tester
	SIP tester, for asterisk
	IRC tester?
	NTP tester?