using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities;

public static class StringExtensions
{
    public static bool HasValue(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? false : true;
    }
    public static string ToLower(this string value)
    {
        Assert.NotNull(name: nameof(value), obj: value, message: "Null Exception");
        return value.Trim().ToLower();
    }
    public static string ToUpper(this string value)
    {
        Assert.NotNull(name: nameof(value), obj: value, message: "Null Exception");
        return value.Trim().ToUpper();
    }
}
