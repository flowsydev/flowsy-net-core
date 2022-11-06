using CaseExtensions;

namespace Flowsy.Core;

public static class NamingExtensions
{
    public static string ApplyNamingConvention(this string str, NamingConvention? convention)
        => convention switch
        {
            NamingConvention.CamelCase => str.ToCamelCase(),
            NamingConvention.PascalCase => str.ToPascalCase(),
            NamingConvention.LowerKebabCase => str.ToKebabCase(),
            NamingConvention.UpperKebabCase => str.ToKebabCase().ToUpper(),
            NamingConvention.LowerSnakeCase => str.ToSnakeCase(),
            NamingConvention.UpperSnakeCase => str.ToSnakeCase().ToUpper(),
            NamingConvention.TrainCase => str.ToTrainCase(),
            _ => str
        };
}