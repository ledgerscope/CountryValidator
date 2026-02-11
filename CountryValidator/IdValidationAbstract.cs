using System;

namespace CountryValidation
{
    public abstract class IdValidationAbstract
    {
        public abstract Country CountryCode { get; }

        public virtual ValidationResult ValidateNationalIdentity(string ssn)
        {
            return ValidateIndividualTaxCode(ssn);
        }

        public abstract ValidationResult ValidateIndividualTaxCode(string id);
        public abstract ValidationResult ValidateEntity(string id);
        public abstract ValidationResult ValidateVAT(string vatId);
        public abstract ValidationResult ValidatePostalCode(string postalCode);

        protected string GetVatNumberRegularized(string vatNumber)
        {
            string regularizedVatNumber = vatNumber.RemoveSpecialCharacters().ToUpper();
            string countryCodeString = this.CountryCode.ToString();
            if (regularizedVatNumber.StartsWith(countryCodeString))
            {
                regularizedVatNumber = regularizedVatNumber.Substring(countryCodeString.Length);
            }
            return regularizedVatNumber;
        }

        protected static string RemoveStringFromStart(string txt, params string[] removals)
        {
            txt = txt?.Trim() ?? string.Empty;
            if (txt.Length > 0)
            {
                foreach (var removal in removals)
                {
                    if (txt.StartsWith(removal, StringComparison.OrdinalIgnoreCase))
                    {
                        txt = txt.Substring(removal.Length);
                        break;
                    }
                }
            }
            return txt;
        }

        protected static string RemoveStringFromEnd(string txt, params string[] removals)
        {
            txt = txt?.Trim() ?? string.Empty;
            if (txt.Length > 0)
            {
                foreach (var removal in removals)
                {
                    if (txt.EndsWith(removal, StringComparison.OrdinalIgnoreCase))
                    {
                        txt = txt.Substring(0, txt.Length - removal.Length);
                        break;
                    }
                }
            }
            return txt;
        }

    }
}
