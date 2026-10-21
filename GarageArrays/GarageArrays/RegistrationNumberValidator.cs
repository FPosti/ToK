namespace GarageArrays;

internal static class RegistrationNumberValidator
{
    public const string ErrorMessage = "Ange ett registreringsnummer i formatet ABC123 eller ABC12A.";

    public static bool TryParse(string? input, out string registrationNumber)
    {
        registrationNumber = string.Empty;
        string candidate = input?.Trim() ?? string.Empty;

        if (candidate.Length != 6)
        {
            return false;
        }

        foreach (char character in candidate[..3])
        {
            if (!char.IsLetter(character))
            {
                return false;
            }
        }

        foreach (char character in candidate[3..5])
        {
            if (!char.IsDigit(character))
            {
                return false;
            }
        }

        if (!char.IsLetterOrDigit(candidate[^1]))
        {
            return false;
        }

        registrationNumber = candidate.ToUpperInvariant();
        return true;
    }
}
