using System.Globalization;
using IRSI.CommonTools.Abstractions;

namespace IRSI.CommonTools;

public class TimeOnlyWrapper : ITimeOnly
{
    public TimeOnly MaxValue => TimeOnly.MaxValue;
    public TimeOnly MinValue => TimeOnly.MinValue;
    public TimeOnly FromDateTime(DateTime dateTime) => TimeOnly.FromDateTime(dateTime);
    public TimeOnly FromTimeSpan(TimeSpan timeSpan) => TimeOnly.FromTimeSpan(timeSpan);

    public TimeOnly Parse(ReadOnlySpan<char> s, IFormatProvider provider, DateTimeStyles style) =>
        TimeOnly.Parse(s, provider, style);

    public TimeOnly Parse(ReadOnlySpan<char> s, IFormatProvider provider) => TimeOnly.Parse(s, provider);
    public TimeOnly Parse(string s, IFormatProvider provider) => TimeOnly.Parse(s, provider);

    public TimeOnly Parse(string s, IFormatProvider provider, DateTimeStyles style) =>
        TimeOnly.Parse(s, provider, style);

    public TimeOnly ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider,
        DateTimeStyles style) => TimeOnly.ParseExact(s, format, provider, style);

    public TimeOnly ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider) =>
        TimeOnly.ParseExact(s, format, provider);

    public TimeOnly ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style) =>
        TimeOnly.ParseExact(s, format, provider, style);

    public TimeOnly ParseExact(string s, string format, IFormatProvider provider) =>
        TimeOnly.ParseExact(s, format, provider);

    public bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, DateTimeStyles style, out TimeOnly result) =>
        TimeOnly.TryParse(s, provider, style, out result);

    public bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, out TimeOnly result) =>
        TimeOnly.TryParse(s, provider, out result);

    public bool TryParse(string s, IFormatProvider provider, DateTimeStyles style, out TimeOnly result) =>
        TimeOnly.TryParse(s, provider, style, out result);

    public bool TryParse(string s, IFormatProvider provider, out TimeOnly result) =>
        TimeOnly.TryParse(s, provider, out result);

    public bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider,
        DateTimeStyles style, out TimeOnly result) => TimeOnly.TryParseExact(s, format, provider, style, out result);

    public bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style,
        out TimeOnly result) => TimeOnly.TryParseExact(s, format, provider, style, out result);
}