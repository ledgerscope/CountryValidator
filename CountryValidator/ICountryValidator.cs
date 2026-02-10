using System.Collections.Generic;

namespace CountryValidation
{
    public interface ICountryValidator
    {
        ValidationResult ValidateEntity(string vat, string countryCode);
        ValidationResult ValidateEntity(string vat, Country country);

        ValidationResult ValidateIndividualTaxCode(string id, string countryCode);
        ValidationResult ValidateIndividualTaxCode(string id, Country country);

        ValidationResult ValidateNationalIdentityCode(string ssn, string countryCode);
        ValidationResult ValidateNationalIdentityCode(string ssn, Country country);

        ValidationResult ValidateVAT(string vat, string countryCode);
        ValidationResult ValidateVAT(string vat, Country country);

        ValidationResult ValidateZIPCode(string zip, string countryCode);
        ValidationResult ValidateZIPCode(string zip, Country country);

        bool TryGetCountryByCode(string code, out Country country);
        bool IsCountrySupported(string countryCode);
        bool IsCountrySupported(Country country);
        IReadOnlyList<Country> SupportedCountryVals { get; }
        List<string> SupportedCountries { get; }
    }
}
