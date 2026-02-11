using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class MalaysiaValidator : IdValidationAbstract
    {
        public override Country CountryCode => Country.MY;

        /// <summary>
        ///   Nombor Cukai Pendapatan (ITN)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacters();

            if (Regex.IsMatch(id, @"^(CS|D|E|F|FA|PT|TA|TC|TN|TR|TP|TJ|LE)\d{10}$"))
            {
                return ValidationResult.InvalidFormat(@"^(CS|D|E|F|FA|PT|TA|TC|TN|TR|TP|TJ|LE)\d{10}$");
            }
            return ValidationResult.Success();

        }


        /// <summary>
        /// Nombor Cukai Pendapatan (ITN)  
        /// </summary>
        /// <param name="itn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string itn)
        {
            itn = itn.RemoveSpecialCharacters();

            if (Regex.IsMatch(itn, @"^(SG|OG)\d{10}[01]$"))
            {
                return ValidationResult.InvalidFormat(@"^(SG|OG)\d{10}[01]$");
            }
            return ValidationResult.Success();

        }

        /// <summary>
        ///  Nombor Cukai Pendapatan (ITN)
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
            if (!Regex.IsMatch(postalCode, "^\\d{5}$"))
            {
                return ValidationResult.InvalidFormat("NNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
