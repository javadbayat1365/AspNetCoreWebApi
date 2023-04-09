using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities;

public static class EnumExtensions
{
    public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentException();
       return Enum.GetValues(input.GetType()).Cast<T>();
    }
    public static string ToDisplay(this Enum input,DisplayProperty displayProperty = DisplayProperty.Name)
    {
        Assert.NotNull(input,nameof(input));
        object? attribute = input.GetType()
                                 .GetField(input.ToString())
                                 .GetCustomAttributes(false)
                                 .FirstOrDefault();
        if (attribute is null)
            return input.ToString();
        var propValue = attribute.GetType().GetProperty(displayProperty.ToString()).GetValue(attribute,null);
        return propValue.ToString();

    }
    public static IDictionary<int,string> ToDictionary(this Enum input)
    {
        return Enum.GetValues(input.GetType()).Cast<Enum>().ToDictionary(p => Int32.Parse(p.ToString()),q => q.ToDisplay());
    }

}
public enum DisplayProperty
{
    Description,
    Name,
    GroupName
}
