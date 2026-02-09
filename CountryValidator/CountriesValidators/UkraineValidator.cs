using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class UkraineValidator : IdValidationAbstract
    {
        public override Country CountryCode => Country.UA;

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacters();
            if (!Regex.IsMatch(id, @"^\d{12}$"))
            {
                return ValidationResult.InvalidFormat("12 digits");
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacters();
            if (!Regex.IsMatch(vatId, @"^\d{12}$"))
            {
                return ValidationResult.InvalidFormat("123456789012");
            }

            return ValidationResult.Success();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacters();
            if (!Regex.IsMatch(postalCode, "^\\d{5}$"))
            {
                return ValidationResult.InvalidFormat("NNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
