using System;
using System.Text;
using System.Text.RegularExpressions;

public static class StringExtensions
{
	/**
	 * @return true if input is a valid URL
	 */
	public static bool IsUrl (this string input)
	{
		string regex =
			"^(https?://){1}"
			+ "(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?"//user@
			+ @"				(([0-9]{1,3}\.){3}[0-9]{1,3}"// IP- 199.194.52.184
			+ "|"// allows either IP or domain
			+ @"				([0-9a-z_!~*'()-]+\.)*"// tertiary domain(s)- www.
			+ @"				([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\."// second level domain
			+ "[a-z]{1,6})"// first level domain- .com or .museum
			+ "(:[0-9]{1,4})?"// port number- :80
			+ "((/?)|"// a slash isn't required if there is no file name
			+ "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";    

		Regex re = new Regex (regex);

		if (re.IsMatch (input))
			return true;

		return false;
	}
}

