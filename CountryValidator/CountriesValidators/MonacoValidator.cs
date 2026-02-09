using System;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class MonacoValidator : IdValidationAbstract
    {
        public override Country CountryCode => Country.MC;

        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotSupportedException();
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            throw new NotSupportedException();
        }

        public override ValidationResult ValidateVAT(string number)
        {
            number = number.RemoveSpecialCharacters();
            number = number.Replace("FR", string.Empty).Replace("fr", string.Empty).Replace("mc", string.Empty).Replace("MC", string.Empty);


            if (number.Substring(2, 3) != "000")
            {
                return ValidationResult.InvalidFormat("Must be line xx000");
            }

            return new FranceValidator().ValidateVAT(number);
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacters();
            if (!Regex.IsMatch(postalCode, "^980\\d{2}$"))
            {
                return ValidationResult.InvalidFormat("980NN");
            }
            return ValidationResult.Success();
        }
    }
}
