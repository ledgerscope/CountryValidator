using System;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class ThailandValidator : IdValidationAbstract
    {
        public override Country CountryCode => Country.TH;

        /// <summary>
        /// Validate Thailand citizen number
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacters();
            if (ssn.Length != 13)
            {
                return ValidationResult.InvalidLength("13 digits");
            }

            var sum = 0;
            for (var i = 0; i < 12; i++)
            {
                sum += (int)Char.GetNumericValue(ssn[i]) * (13 - i);
            }

            return (11 - sum % 11).Mod(10) == (int)char.GetNumericValue(ssn[12]) ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateIndividualTaxCode(id);
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacters();
            if (ValidateNationalIdentity(ssn).IsValid)
            {
                return ValidationResult.Success();
            }
            else if (!Regex.IsMatch(ssn, @"^\d{10}$"))
            {
                return ValidationResult.Success();

            }
            return ValidationResult.InvalidOther("Invalid TIN");

        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            return ValidateIndividualTaxCode(vatId);
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
