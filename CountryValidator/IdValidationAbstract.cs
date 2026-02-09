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
    }
}
