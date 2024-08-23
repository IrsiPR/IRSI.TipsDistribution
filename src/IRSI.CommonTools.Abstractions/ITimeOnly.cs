using System.Globalization;

namespace IRSI.CommonTools.Abstractions;

public interface ITimeOnly
{
    TimeOnly MaxValue { get; }
    TimeOnly MinValue { get; }
    TimeOnly FromDateTime(DateTime dateTime);
    TimeOnly FromTimeSpan(TimeSpan timeSpan);
    TimeOnly Parse(ReadOnlySpan<char> s, IFormatProvider provider, DateTimeStyles style);
    TimeOnly Parse(ReadOnlySpan<char> s, IFormatProvider provider);
    TimeOnly Parse(string s, IFormatProvider provider);
    TimeOnly Parse(string s, IFormatProvider provider, DateTimeStyles style);

    TimeOnly ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider,
        DateTimeStyles style);

    TimeOnly ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider);
    TimeOnly ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style);
    TimeOnly ParseExact(string s, string format, IFormatProvider provider);
    bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, DateTimeStyles style, out TimeOnly result);
    bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, out TimeOnly result);
    bool TryParse(string s, IFormatProvider provider, DateTimeStyles style, out TimeOnly result);
    bool TryParse(string s, IFormatProvider provider, out TimeOnly result);

    bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider,
        DateTimeStyles style, out TimeOnly result);

    bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style,
        out TimeOnly result);
}