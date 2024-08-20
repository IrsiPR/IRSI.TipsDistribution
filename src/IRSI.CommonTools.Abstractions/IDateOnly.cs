using System.Globalization;

namespace IRSI.CommonTools.Abstractions;

public interface IDateOnly
{
    DateOnly MaxValue { get; }
    DateOnly MinValue { get; }
    DateOnly FromDateTime(DateTime dateTime);
    DateOnly FromDayNumber(int dayNumber);
    DateOnly Parse(ReadOnlySpan<char> s, IFormatProvider provider, DateTimeStyles style);
    DateOnly Parse(ReadOnlySpan<char> s, IFormatProvider provider);
    DateOnly Parse(string s, IFormatProvider provider);
    DateOnly Parse(string s, IFormatProvider provider, DateTimeStyles style);

    DateOnly ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider,
        DateTimeStyles style);

    DateOnly ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider);
    DateOnly ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style);
    DateOnly ParseExact(string s, string format, IFormatProvider provider);
    bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, DateTimeStyles style, out DateOnly result);
    bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, out DateOnly result);
    bool TryParse(string s, IFormatProvider provider, DateTimeStyles style, out DateOnly result);
    bool TryParse(string s, IFormatProvider provider, out DateOnly result);

    bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider,
        DateTimeStyles style, out DateOnly result);

    bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style,
        out DateOnly result);
}