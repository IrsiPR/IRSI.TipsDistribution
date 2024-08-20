using System.Globalization;

namespace IRSI.CommonTools.Abstractions;

public interface IDateTime
{
    DateTime MaxValue { get; }
    DateTime MinValue { get; }
    DateTime Now { get; }
    DateTime Today { get; }
    DateTime UtcNow { get; }
    DateTime UnixEpoch { get; }
    bool IsLeapYear(int year);
    DateTime FromBinary(long dateData);
    DateTime FromFileTime(long fileTime);
    DateTime FromFileTimeUtc(long fileTime);
    DateTime FromOADate(double d);
    DateTime SpecifyKind(DateTime value, DateTimeKind kind);
    int Compare(DateTime t1, DateTime t2);
    int DaysInMonth(int year, int month);
    bool Equals(DateTime t1, DateTime t2);
    DateTime Parse(string s);
    DateTime Parse(string s, IFormatProvider provider);
    DateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles);
    DateTime ParseExact(string s, string format, IFormatProvider provider);
    DateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style);
    DateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style);
    bool TryParse(string s, out DateTime result);
    bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out DateTime result);
    bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out DateTime result);
    bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result);
}