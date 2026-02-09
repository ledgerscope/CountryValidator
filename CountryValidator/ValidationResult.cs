namespace CountryValidation
{
    public sealed record ValidationResult
    {

        public bool IsValid { get; }

        public string ErrorMessage { get; }

        public ValidationResult(bool isValid, string errorMessage)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

        public static ValidationResult Success()
        {
            return new ValidationResult(true, string.Empty);
        }

        public static ValidationResult Invalid(string errorMessage)
        {
            return new ValidationResult(false, errorMessage);
        }

        public static ValidationResult InvalidChecksum()
        {
            return new ValidationResult(false, "Invalid checksum.");
        }
        public static ValidationResult InvalidFormat(string format)
        {
            return new ValidationResult(false, $"Invalid format. The code must have this format {format}");
        }
        public static ValidationResult InvalidDate()
        {
            return new ValidationResult(false, "Invalid date");
        }

        public static ValidationResult InvalidLength()
        {
            return new ValidationResult(false, "Invalid length");
        }
    }
}
