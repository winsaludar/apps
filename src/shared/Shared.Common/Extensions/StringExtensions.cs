namespace Shared.Common.Extensions;

public static class StringExtensions
{
    public static string RemoveNewLine(this string s)
    {
        return s.Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", "");
    }
}
