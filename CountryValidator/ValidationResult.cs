using System;

namespace CountryValidation
{
    public sealed record ValidationResult
    {
        public ValidationStatus Validity { get; }

        public bool IsValid => Validity == ValidationStatus.Valid;

        public string ErrorMessage { get; }

        public ValidationResult(ValidationStatus validity, string? info = null)
        {
            Validity = validity;
            ErrorMessage = getErrorMessage(validity, info);
        }

        private string getErrorMessage(ValidationStatus validity, string? info)
        {
            switch(validity)
            {
                case ValidationStatus.Valid:
                    return string.Empty;
                case ValidationStatus.InvalidChecksum:
                    return "Invalid checksum.";
                case ValidationStatus.InvalidFormat:
                    return $"Does not conform to format: {info}";
                case ValidationStatus.InvalidDate:
                    return "Invalid date";
                case ValidationStatus.InvalidLength:
                    return $"Invalid length. Required length: {info}";
                case ValidationStatus.InvalidOther:
                    return info ?? string.Empty;
                case ValidationStatus.CountryNotSupported:
                    return $"Country not supported: {info}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(validity), validity, null);
            }
        }

        #region Factory Methods
        public static ValidationResult Success()
        {
            return new ValidationResult(ValidationStatus.Valid);
        }

        public static ValidationResult InvalidChecksum()
        {
            return new ValidationResult(ValidationStatus.InvalidChecksum);
        }

        //public static ValidationResult Invalid(string format)
        //    => InvalidFormat(format);

        public static ValidationResult InvalidFormat(string info)
        {
            return new ValidationResult(ValidationStatus.InvalidFormat, info);
        }

        public static ValidationResult InvalidDate()
        {
            return new ValidationResult(ValidationStatus.InvalidDate);
        }

        public static ValidationResult InvalidLength(string info)
        {
            return new ValidationResult(ValidationStatus.InvalidLength, info);
        }

        public static ValidationResult InvalidOther(string info)
        {
            return new ValidationResult(ValidationStatus.InvalidOther, info);
        }

        public static ValidationResult CountryNotSupported(Country country)
        {
            return new ValidationResult(ValidationStatus.CountryNotSupported, country.ToString());
        }

        public static ValidationResult CountryNotSupported(string countryCode)
        {
            return new ValidationResult(ValidationStatus.CountryNotSupported, countryCode);
        }
        #endregion
    }
}
