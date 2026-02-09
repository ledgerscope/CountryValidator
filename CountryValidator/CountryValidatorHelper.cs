using System.Collections.Generic;

namespace CountryValidation
{
	public static class CountryValidatorHelper 
	{
		// The CountryValidator class has no state and should have been a static in the first place,
		// but to avoid breaking changes, and to avoid getting too far adrift from the repo that
		// we are forked from, we will keep it as is and wrap an instance.

		private static readonly CountryValidator _instance = new CountryValidator();

		public static bool IsCountrySupported(Country country)
			=> CountryValidator.IsCountrySupported(country);


		public static List<string> SupportedCountries
			=> CountryValidator.SupportedCountries;

		public static ValidationResult ValidateIndividualTaxCode(string ssn, string countryCode)
			=> _instance.ValidateIndividualTaxCode(ssn, countryCode);
		public static ValidationResult ValidateIndividualTaxCode(string ssn, Country country)
			=> _instance.ValidateIndividualTaxCode(ssn, country);

		public static ValidationResult ValidateVAT(string vat, string countryCode)
			=> _instance.ValidateVAT(vat, countryCode);
		public static ValidationResult ValidateVAT(string vat, Country country)
			=> _instance.ValidateVAT(vat, country);

		public static ValidationResult ValidateEntity(string vat, string countryCode)
			=> _instance.ValidateEntity(vat, countryCode);
		public static ValidationResult ValidateEntity(string vat, Country country)
			=> _instance.ValidateEntity(vat, country);

		public static ValidationResult ValidateNationalIdentityCode(string ssn, string countryCode)
			=> _instance.ValidateNationalIdentityCode(ssn, countryCode);
		public static ValidationResult ValidateNationalIdentityCode(string ssn, Country country)
			=> _instance.ValidateNationalIdentityCode(ssn, country);

		public static ValidationResult ValidateZIPCode(string zip, string countryCode)
			=> _instance.ValidateZIPCode(zip, countryCode);
		public static ValidationResult ValidateZIPCode(string zip, Country country)
			=> _instance.ValidateZIPCode(zip, country);
	}
}
