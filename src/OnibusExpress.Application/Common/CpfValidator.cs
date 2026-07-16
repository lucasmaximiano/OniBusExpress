
namespace OnibusExpress.Application.Common
{
    public static class CpfValidator
    {
        public static bool IsValid(string? cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            var digits = new string(cpf.Where(char.IsDigit).ToArray());

            if (digits.Length != 11)
                return false;

            if (digits.Distinct().Count() == 1)
                return false;

            var firstDigit = CalculateDigit(digits[..9], 10);
            var secondDigit = CalculateDigit(digits[..10], 11);

            return digits[9] - '0' == firstDigit
                && digits[10] - '0' == secondDigit;
        }

        private static int CalculateDigit(string digits, int initialWeight)
        {
            var sum = 0;

            for (var index = 0; index < digits.Length; index++)
            {
                sum += (digits[index] - '0') * (initialWeight - index);
            }

            var remainder = sum % 11;

            return remainder < 2 ? 0 : 11 - remainder;
        }
    }
}
