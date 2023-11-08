using CaseExtensions;

namespace Flowsy.Core;

public static class StringExtensions
{
    public static string ApplyConvention(this string str, CaseConvention? convention)
        => convention switch
        {
            CaseConvention.LowerCase => str.ToLower(),
            CaseConvention.UpperCase => str.ToUpper(),
            CaseConvention.CamelCase => str.ToCamelCase(),
            CaseConvention.PascalCase => str.ToPascalCase(),
            CaseConvention.LowerKebabCase => str.ToKebabCase(),
            CaseConvention.UpperKebabCase => str.ToKebabCase().ToUpper(),
            CaseConvention.LowerSnakeCase => str.ToSnakeCase(),
            CaseConvention.UpperSnakeCase => str.ToSnakeCase().ToUpper(),
            CaseConvention.TrainCase => str.ToTrainCase(),
            _ => str
        };
    
    public static bool TryConvert<T>(this string str, out T? value)
    {
        try
        {
            var converted = str.TryConvert(typeof(T), out var v);
            value = v is null ? default : (T) v;
            return converted;
        }
        catch
        {
            value = default;
            return false;
        }
    }
    
    public static bool TryConvert(this string str, Type type, out object? value)
    {
        try
        {
            if (type == typeof(short))
                value = Convert.ToInt16(str);
            else if (type == typeof(int))
                value = Convert.ToInt32(str);
            else if (type == typeof(long))
                value = Convert.ToInt64(str);
            else if (type == typeof(float))
                value = Convert.ToSingle(str);
            else if (type == typeof(double))
                value = Convert.ToDouble(str);
            else if (type == typeof(decimal))
                value = Convert.ToDecimal(str);
            else if (type == typeof(bool))
                value = Convert.ToBoolean(str);
            else
                value = null;

            return true;
        }
        catch (Exception)
        {
            value = null;
            return false;
        }
    }
}