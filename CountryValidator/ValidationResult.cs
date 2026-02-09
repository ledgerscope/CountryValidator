using System;

namespace CountryValidation
{
    public sealed record ValidationResult
    {
        public ValidationStatus Validity { get; }

        public bool IsValid => Validity == ValidationStatus.Valid;

        public string ErrorMessage { get; }

        public ValidationResult(ValidationStatus validity, string? format = null)
        {
            Validity = validity;
            ErrorMessage = getErrorMessage(validity, format);
        }

        private string getErrorMessage(ValidationStatus validity, string? format)
        {
            switch(validity)
            {
                case ValidationStatus.Valid:
                    return string.Empty;
                case ValidationStatus.InvalidChecksum:
                    return "Invalid checksum.";
                case ValidationStatus.InvalidFormat:
                    return format ?? string.Empty;
                case ValidationStatus.InvalidDate:
                    return "Invalid date";
                case ValidationStatus.InvalidLength:
                    return "Invalid length";
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

        public static ValidationResult Invalid(string format)
            => InvalidFormat(format);

        public static ValidationResult InvalidFormat(string format)
        {
            return new ValidationResult(ValidationStatus.InvalidFormat, format);
        }

        public static ValidationResult InvalidDate()
        {
            return new ValidationResult(ValidationStatus.InvalidDate);
        }

        public static ValidationResult InvalidLength()
        {
            return new ValidationResult(ValidationStatus.InvalidLength);
        }
        #endregion
    }
}
