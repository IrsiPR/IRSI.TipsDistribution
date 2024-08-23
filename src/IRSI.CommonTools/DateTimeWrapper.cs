using System.Globalization;
using IRSI.CommonTools.Abstractions;

namespace IRSI.CommonTools;

public class DateTimeWrapper : IDateTime
{
    public DateTime MaxValue => DateTime.MaxValue;
    public DateTime MinValue => DateTime.MinValue;
    public DateTime Now => DateTime.Now;
    public DateTime Today => DateTime.Today;
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime UnixEpoch => DateTime.UnixEpoch;
    public bool IsLeapYear(int year) => DateTime.IsLeapYear(year);
    public DateTime FromBinary(long dateData) => DateTime.FromBinary(dateData);
    public DateTime FromFileTime(long fileTime) => DateTime.FromFileTime(fileTime);
    public DateTime FromFileTimeUtc(long fileTime) => DateTime.FromFileTimeUtc(fileTime);
    public DateTime FromOADate(double d) => DateTime.FromOADate(d);
    public DateTime SpecifyKind(DateTime value, DateTimeKind kind) => DateTime.SpecifyKind(value, kind);
    public int Compare(DateTime t1, DateTime t2) => DateTime.Compare(t1, t2);
    public int DaysInMonth(int year, int month) => DateTime.DaysInMonth(year, month);
    public bool Equals(DateTime t1, DateTime t2) => DateTime.Equals(t1, t2);
    public DateTime Parse(string s) => DateTime.Parse(s);
    public DateTime Parse(string s, IFormatProvider provider) => DateTime.Parse(s, provider);

    public DateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles) =>
        DateTime.Parse(s, provider, styles);

    public DateTime ParseExact(string s, string format, IFormatProvider provider) =>
        DateTime.ParseExact(s, format, provider);

    public DateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style) =>
        DateTime.ParseExact(s, format, provider, style);

    public DateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style) =>
        DateTime.ParseExact(s, formats, provider, style);

    public bool TryParse(string s, out DateTime result) => DateTime.TryParse(s, out result);

    public bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out DateTime result) =>
        DateTime.TryParse(s, provider, styles, out result);

    public bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style,
        out DateTime result) => DateTime.TryParseExact(s, format, provider, style, out result);

    public bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style,
        out DateTime result) => DateTime.TryParseExact(s, formats, provider, style, out result);
}