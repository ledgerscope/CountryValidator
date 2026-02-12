using System;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class PakistanValidator : IdValidationAbstract
    {
        public override Country CountryCode => Country.PK;

        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Validate CNIC (Computerized National Identity Card)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.Trim();

            var isValid = Regex.IsMatch(id, "^[1-7][0-9]{4}-[0-9]{7}-[1-9]{1}$");
            if (isValid)
            {
                return ValidationResult.Success();
            }
            else
            {
                return ValidationResult.InvalidFormat("^[1-7][0-9]{4}-[0-9]{7}-[1-9]{1}$");
            }
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotSupportedException($"{this.CountryCode} validator does not support VAT validation.");
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
