namespace WebUI.Client.Extensions
{
    public static class IntExtensions
    {
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
}
