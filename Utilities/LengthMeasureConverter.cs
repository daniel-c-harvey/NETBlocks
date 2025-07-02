using System;
using System.Text.RegularExpressions;
namespace NetBlocks.Utilities;

public static class LengthMeasureConverter
{
    private static readonly Regex ImperialLengthPattern = new Regex(
        @"^(?:(?<feet>\d+(?:\.\d+)?)'?\s*)?(?:(?<inches>\d+(?:\.\d+)?)\""?\s*)?(?<fraction>\d+/\d+)?$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Converts imperial unit strings (e.g., "6' 8\"1/4", "72\"", "6'", "1/2\"") to decimal feet
    /// </summary>
    /// <param name="imperialString">Imperial measurement string</param>
    /// <returns>Decimal feet value</returns>
    /// <exception cref="ArgumentException">Thrown when input format is invalid</exception>
    public static decimal ToDecimalFeet(string imperialString)
    {
        if (string.IsNullOrWhiteSpace(imperialString))
            throw new ArgumentException("Input cannot be null or empty", nameof(imperialString));

        var match = ImperialLengthPattern.Match(imperialString.Trim());
        if (!match.Success)
            throw new ArgumentException($"Invalid imperial format: {imperialString}", nameof(imperialString));

        decimal feet = ParseComponent(match.Groups["feet"].Value);
        decimal inches = ParseComponent(match.Groups["inches"].Value);
        decimal fraction = ParseFraction(match.Groups["fraction"].Value);

        return feet + (inches + fraction) / 12m;
    }

    /// <summary>
    /// Converts decimal feet to imperial format string (e.g., 6.6875 → "6' 8 1/4\"")
    /// </summary>
    /// <param name="decimalFeet">Decimal feet value</param>
    /// <param name="precision">Fractional precision (4, 8, 16, 32, 64)</param>
    /// <returns>Formatted imperial string</returns>
    public static string ToImperialString(decimal decimalFeet, int precision = 16)
    {
        if (decimalFeet < 0)
            throw new ArgumentException("Value cannot be negative", nameof(decimalFeet));

        if (!IsPowerOfTwo(precision) || precision < 2 || precision > 64)
            throw new ArgumentException("Precision must be a power of 2 between 2 and 64", nameof(precision));

        int wholeFeet = (int)decimalFeet;
        decimal remainingInches = (decimalFeet - wholeFeet) * 12m;
        int wholeInches = (int)remainingInches;
        decimal fractionalInches = remainingInches - wholeInches;

        var parts = new System.Collections.Generic.List<string>();

        if (wholeFeet > 0)
            parts.Add($"{wholeFeet}'");

        if (wholeInches > 0 || fractionalInches > 0 || wholeFeet == 0)
        {
            string inchPart = wholeInches.ToString();
            
            if (fractionalInches > 0)
            {
                var (numerator, denominator) = ToSimplestFraction(fractionalInches, precision);
                if (numerator > 0)
                    inchPart += $" {numerator}/{denominator}";
            }
            
            parts.Add($"{inchPart}\"");
        }

        return string.Join(" ", parts);
    }

    private static decimal ParseComponent(string value)
    {
        return string.IsNullOrEmpty(value) ? 0m : decimal.Parse(value);
    }

    private static decimal ParseFraction(string fraction)
    {
        if (string.IsNullOrEmpty(fraction))
            return 0m;

        var parts = fraction.Split('/');
        if (parts.Length != 2)
            throw new ArgumentException($"Invalid fraction format: {fraction}");

        return decimal.Parse(parts[0]) / decimal.Parse(parts[1]);
    }

    private static (int numerator, int denominator) ToSimplestFraction(decimal value, int precision)
    {
        int numerator = (int)Math.Round(value * precision);
        int denominator = precision;

        // Reduce to simplest form
        int gcd = GreatestCommonDivisor(Math.Abs(numerator), denominator);
        return (numerator / gcd, denominator / gcd);
    }

    private static int GreatestCommonDivisor(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private static bool IsPowerOfTwo(int value)
    {
        return value > 0 && (value & (value - 1)) == 0;
    }
}