using System.Globalization;
using IRSI.CommonTools.Abstractions;

namespace IRSI.CommonTools;

public class DateOnlyWrapper : IDateOnly
{
    public DateOnly MaxValue => DateOnly.MaxValue;
    public DateOnly MinValue => DateOnly.MinValue;
    public DateOnly FromDateTime(DateTime dateTime) => DateOnly.FromDateTime(dateTime);
    public DateOnly FromDayNumber(int dayNumber) => DateOnly.FromDayNumber(dayNumber);

    public DateOnly Parse(ReadOnlySpan<char> s, IFormatProvider provider, DateTimeStyles style) =>
        DateOnly.Parse(s, provider, style);

    public DateOnly Parse(ReadOnlySpan<char> s, IFormatProvider provider) => DateOnly.Parse(s, provider);
    public DateOnly Parse(string s, IFormatProvider provider) => DateOnly.Parse(s, provider);

    public DateOnly Parse(string s, IFormatProvider provider, DateTimeStyles style) =>
        DateOnly.Parse(s, provider, style);

    public DateOnly ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider,
        DateTimeStyles style) => DateOnly.ParseExact(s, format, provider, style);

    public DateOnly ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider) =>
        DateOnly.ParseExact(s, format, provider);

    public DateOnly ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style) =>
        DateOnly.ParseExact(s, format, provider, style);

    public DateOnly ParseExact(string s, string format, IFormatProvider provider) =>
        DateOnly.ParseExact(s, format, provider);

    public bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, DateTimeStyles style, out DateOnly result) =>
        DateOnly.TryParse(s, provider, style, out result);

    public bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, out DateOnly result) =>
        DateOnly.TryParse(s, provider, out result);

    public bool TryParse(string s, IFormatProvider provider, DateTimeStyles style, out DateOnly result) =>
        DateOnly.TryParse(s, provider, style, out result);

    public bool TryParse(string s, IFormatProvider provider, out DateOnly result) =>
        DateOnly.TryParse(s, provider, out result);

    public bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider,
        DateTimeStyles style, out DateOnly result) => DateOnly.TryParseExact(s, format, provider, style, out result);

    public bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style,
        out DateOnly result) => DateOnly.TryParseExact(s, format, provider, style, out result);
}