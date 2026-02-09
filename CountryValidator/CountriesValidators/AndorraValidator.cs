using System.Linq;
using System.Text.RegularExpressions;


namespace CountryValidation.Countries
{
    public class AndorraValidator : IdValidationAbstract
    {
        public override Country CountryCode => Country.AD;

        /// <summary>
        /// NRT (Número de Registre Tributari, Andorra tax number)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateIndividualTaxCode(id);
        }

        /// <summary>
        /// NRT (Número de Registre Tributari, Andorra tax number)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// https://www.oecd.org/tax/automatic-exchange/crs-implementation-and-assistance/tax-identification-numbers/Andorra-TIN.pdf
        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacters().Replace("AD", string.Empty).Replace("ad", string.Empty);

            if (id.Length != 8)
            {
                return ValidationResult.InvalidLength("8 characters");
            }

            if (!char.IsLetter(id[0]) || !char.IsLetter(id[id.Length - 1]))
            {
                return ValidationResult.InvalidFormat("First and last character must be letters");
            }
            else if (!id.Substring(1, 6).All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("F-123456-Z");
            }
            else if (!Regex.IsMatch(id, "^[ACDEFGLOPU]"))
            {
                return ValidationResult.InvalidFormat("First letter must be ACDEFGLOPU");
            }
            else if (id[0] == 'F' && int.Parse(id.Substring(1, 6)) > 699999)
            {
                return ValidationResult.InvalidFormat("The number code cannot be higher than 699999");
            }
            if ((id[0] == 'A' || id[0] == 'L') && !(699999 < int.Parse(id.Substring(1, 6)) && int.Parse(id.Substring(1, 6)) < 800000))
            {
                return ValidationResult.InvalidFormat("The number code must be between 699999 and 800000");
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// NRT (Número de Registre Tributari, Andorra tax number)
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            return ValidateIndividualTaxCode(vatId);
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacters();
            if (!Regex.IsMatch(postalCode, "^[Aa][Dd]\\d{3}$"))
            {
                return ValidationResult.InvalidFormat("CCNNN");
            }
            return ValidationResult.Success();
        }
    }
}
