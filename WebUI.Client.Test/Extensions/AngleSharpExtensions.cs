using AngleSharp.Dom;

namespace WebUI.Client.Test.Extensions;

public static class AngleSharpExtensions
{
    public static string TrimmedText(this IElement self)
    {
        return self.TextContent.Trim();
    }
}
