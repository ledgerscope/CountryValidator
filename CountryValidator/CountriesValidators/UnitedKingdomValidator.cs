using System;
using System.Linq;
using System.Text.RegularExpressions;


namespace CountryValidation.Countries
{
    public class UnitedKingdomValidator : IdValidationAbstract
    {
        public override Country CountryCode => Country.GB;

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        /// <summary>
        /// NINO or NHS
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string ssn)
        {
            ssn = ssn?.RemoveSpecialCharacters() ?? string.Empty;
            if (ValidateIndividualTaxCode(ssn).IsValid)
            {
                return ValidationResult.Success();
            }
            else if (ValidateNHS(ssn).IsValid)
            {
                return ValidationResult.Success();
            }
            return ValidationResult.InvalidOther("Not individual tax code and not NHS code");
        }

        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return ValidationResult.InvalidFormat("Must not be empty");
            }
            bool isValid = Regex.IsMatch(id, "^(?!BG|GB|NK|KN|TN|NT|ZZ)((?![DFIQUV])([A-Z])(?![DFIQUVO])([A-Z]))[0-9]{6}[A-D ]$");
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidFormat("^(?!BG|GB|NK|KN|TN|NT|ZZ)((?![DFIQUV])([A-Z])(?![DFIQUVO])([A-Z]))[0-9]{6}[A-D ]$");
        }

        public ValidationResult ValidateNHS(string ssn)
        {
            ssn = ssn?.RemoveSpecialCharacters() ?? string.Empty;
            if (ssn.Length != 10 || !ssn.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("1234567890");
            }

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(ssn[i].ToString()) * (10 - i);
            }
            sum = sum % 11;
            sum = 11 - sum;
            if (sum == 11)
            {
                sum = 0;
            }
            else if (sum == 10)
            {
                return ValidationResult.InvalidChecksum();
            }

            bool isValid = int.Parse(ssn[9].ToString()) == sum;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Value added tax registration number 
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = GetVatNumberRegularized(vatId);

            if (vatId.StartsWith("GB", StringComparison.OrdinalIgnoreCase))
                vatId = vatId.Substring(2);

            if (vatId.Length < 4)
            {
                return ValidationResult.InvalidLength("Must be at least 4 characters");
            }

            if (vatId.Substring(0, 2) == "GD")
            {
                bool isValidGD = int.Parse(vatId.Substring(2, 3)) < 500;
                return isValidGD ? ValidationResult.Success() : ValidationResult.InvalidFormat("Bad format for if starts with 'GD'");
            }
            else if (vatId.Substring(0, 2) == "HA")
            {
                bool isValidHA = int.Parse(vatId.Substring(2, 3)) > 499;
                return isValidHA ? ValidationResult.Success() : ValidationResult.InvalidFormat("Bad format for if starts with 'HA'");
            }

            var total = 0;
            if (vatId[0] == '0')
            {
                return ValidationResult.InvalidFormat("First digit cannot be 0");
            }

            if (vatId.Length != 9)
            {
                return ValidationResult.InvalidLength("9 characters");
            }

            if (Regex.IsMatch(vatId, "[a-zA-Z]"))
            {
                return ValidationResult.InvalidFormat("Cannot contain letters (other than starting 'GB')");
            }

            var no = long.Parse(vatId.Substring(0, 7));

            var multipliers = new int[] { 8, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 7; i++)
            {
                total += int.Parse(vatId[i].ToString()) * multipliers[i];
            }

            int cd = total;
            while (cd > 0)
            {
                cd -= 97;
            }

            cd = Math.Abs(cd);
            if (cd == int.Parse(vatId.Substring(7, 2)) && no < 9990001 && (no < 100000 || no > 999999) && (no < 9490001 || no > 9700000))
            {
                return ValidationResult.Success();
            }

            cd = cd >= 55 ? cd - 55 : cd + 42;

            bool isValid = cd == int.Parse(vatId.Substring(7, 2)) && no > 1000000;

            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacters();
            if (!Regex.IsMatch(postalCode, "^GIR ?0AA|(?:(?:AB|AL|B|BA|BB|BD|BH|BL|BN|BR|BS|BT|BX|CA|CB|CF|CH|CM|CO|CR|CT|CV|CW|DA|DD|DE|DG|DH|DL|DN|DT|DY|E|EC|EH|EN|EX|FK|FY|G|GL|GY|GU|HA|HD|HG|HP|HR|HS|HU|HX|IG|IM|IP|IV|JE|KA|KT|KW|KY|L|LA|LD|LE|LL|LN|LS|LU|M|ME|MK|ML|N|NE|NG|NN|NP|NR|NW|OL|OX|PA|PE|PH|PL|PO|PR|RG|RH|RM|S|SA|SE|SG|SK|SL|SM|SN|SO|SP|SR|SS|ST|SW|SY|TA|TD|TF|TN|TQ|TR|TS|TW|UB|W|WA|WC|WD|WF|WN|WR|WS|WV|YO|ZE)(?:\\d[\\dA-Z]? ?\\d[ABD-HJLN-UW-Z]{2}))|BFPO ?\\d{1,4}$"))
            {
                return ValidationResult.InvalidFormat("W(W)N(W/N)NWW OR (W[W]N[W/N] NWW)");
            }
            return ValidationResult.Success();
        }
    }
}
