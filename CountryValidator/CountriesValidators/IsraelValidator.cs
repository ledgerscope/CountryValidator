using System.Linq;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class IsraelValidator : IdValidationAbstract
    {
        public override Country CountryCode => Country.IL;

        public override ValidationResult ValidateEntity(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacters() ?? string.Empty;
            if (ssn.Length > 9)
            {
                return ValidationResult.InvalidLength("9 digits or less");
            }
            if (!ssn.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("Must be all digits");
            }
            else if (ssn.Length < 9)
            {
                ssn = ssn.PadLeft(9, '0');
            }

            if (!Regex.IsMatch(ssn, @"^0*5\d+$"))
            {
                return ValidationResult.InvalidFormat("For companies the first digit must be 5");
            }
            else if (!ssn.CheckLuhnDigit())
            {
                return ValidationResult.InvalidChecksum();
            }

            return ValidationResult.Success();
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacters() ?? string.Empty;
            if (ssn.Length != 9)
            {
                return ValidationResult.InvalidLength("9 digits");
            }

            int counter = 0;
            for (int i = 0; i < 9; i++)
            {
                int incNum = (int)char.GetNumericValue(ssn[i]);
                incNum *= (i % 2) + 1;
                if (incNum > 9)
                {
                    incNum -= 9;
                }

                counter += incNum;
            }
            bool isValid = counter % 10 == 0;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Company Number
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = GetVatNumberRegularized(vatId);
            return ValidateEntity(vatId);
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacters();
            if (!Regex.IsMatch(postalCode, "^\\d{7}$"))
            {
                return ValidationResult.InvalidFormat("NNNNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
