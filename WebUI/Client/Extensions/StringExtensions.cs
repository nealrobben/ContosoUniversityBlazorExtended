namespace WebUI.Client.Extensions;

public static class StringExtensions
{
    public static string AppendParameter(this string value, string parameterName, string parameterValue)
    {
        if (!string.IsNullOrWhiteSpace(parameterValue))
        {
            var separator = !value.Contains('?') ? '?' : '&';
            return $"{value}{separator}{parameterName}={parameterValue}";
        }

        return value;
    }

    public static string AppendParameter(this string value, string parameterName, int? parameterValue)
    {
        if (parameterValue != null)
        {
            var separator = !value.Contains('?') ? '?' : '&';
            return $"{value}{separator}{parameterName}={parameterValue}";
        }

        return value;
    }
}
