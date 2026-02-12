using System.Linq;

namespace CountryValidation.Extensions
{
	public static class StringExtensionMethods
	{
		public static bool IsAllDigits(this string txt)
			=> txt.All(char.IsDigit);
	}
}
