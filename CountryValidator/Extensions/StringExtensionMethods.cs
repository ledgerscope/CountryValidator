using System.Linq;

namespace CountryValidation.Extensions
{
	internal static class StringExtensionMethods
	{
		internal static bool IsAllDigits(this string txt)
			=> txt.All(char.IsDigit);
	}
}
