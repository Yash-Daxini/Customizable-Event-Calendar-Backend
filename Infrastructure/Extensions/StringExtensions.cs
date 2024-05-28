namespace Infrastructure.Extensions;

public static class StringExtensions
{
    public static TEnum ToEnum<TEnum>(this string value)
    {
        return (TEnum)Enum.Parse(typeof(TEnum), value);
    }
}
