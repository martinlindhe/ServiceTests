== About ==

HTTP server "black box" unit test suite for .NET
"Unit Test" a deployed system by describing "known" operation
as tests.

Examples:
* verify that your website stays configured correctly to handle permanent redirects
* verify that the served SSL certificate is valid
* verify that JSON api is working as expected
* test that current DEV/LIVE/EXTERNAL system configuration assumptions stays true

The tests should work for any HTTP/HTTPS web server.


    
Idea from http://robb.weblaws.org/2014/01/16/new-open-source-library-for-test-driven-devops/, https://news.ycombinator.com/item?id=7074942
Something similar, for Ruby (?): https://github.com/hannestyden/hyperspec


== TODO core ==
    * what .NET http libs to use?
    * add a "webpage load time test". load page 10 times, show average load speed
    * can we extend NUnit ?
    * make unit tests pass
    * write a mini website to use as reference, using .NET web classes?

== TODO new modules ==
    MySQL tester?
	SSH tester?
	FTP tester?
	SMTP tester? for postfix? exim4?
	SIP tester? for asterisk