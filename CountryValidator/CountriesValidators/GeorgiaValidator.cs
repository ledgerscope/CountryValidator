using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class GeorgiaValidator : IdValidationAbstract
    {
        public override Country CountryCode => Country.GE;

        public override ValidationResult ValidateEntity(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacters();
            if (!Regex.IsMatch(ssn, @"^\d{9}$"))
            {
                return ValidationResult.InvalidFormat(@"^\d{9}$");
            }
            return ValidationResult.Success();

        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacters();
            if (!Regex.IsMatch(ssn, @"^(\d{9}|\d{11})$"))
            {
                return ValidationResult.InvalidFormat(@"^(\d{9}|\d{11})$");
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = GetVatNumberRegularized(vatId);
            return ValidateEntity(vatId);
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacters();
            if (!Regex.IsMatch(postalCode, "^\\d{4}$"))
            {
                return ValidationResult.InvalidFormat("NNNN");
            }
            return ValidationResult.Success();
        }
    }
}
