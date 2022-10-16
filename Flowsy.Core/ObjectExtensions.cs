using System.Reflection;

namespace Flowsy.Core;

public static class ObjectExtensions
{
    public static IDictionary<string, object?> ToDictionary(this object obj)
    {
        var dictionary = new Dictionary<string, object?>();
        
        foreach (var property in obj.GetType().GetRuntimeProperties())
            dictionary[property.Name] = property.GetValue(obj);
        
        return dictionary;
    }

    public static IReadOnlyDictionary<string, object?> ToReadonlyDictionary(this object obj)
        => (IReadOnlyDictionary<string, object?>) ToDictionary(obj);

    public static void SetProperty(this object obj, string propertyName, object? value)
    {
        var type = obj.GetType();
        var property = type.GetProperty(propertyName);
        if (property is null) 
            return;

        SetProperty(obj, property, value);
    }

    private static void SetProperty(object obj, PropertyInfo property, object? value)
    {
        if (value is null || property.PropertyType == value.GetType())
        {
            property.SetValue(obj, value);
            return;
        }

        var stringValue = value.ToString();
        if (stringValue is null)
        {
            property.SetValue(obj, null);
            return;
        }
        
        property.SetValue(obj, stringValue.TryConvert(property.PropertyType, out var v) ? v : null);
    }

    public static void SetProperties(this object obj, IReadOnlyDictionary<string, object?> data)
    {
        var type = obj.GetType();
        foreach (var (name, value) in data)
        {
            var property = type.GetProperty(name);
            if (property is null)
                continue;
            
            SetProperty(obj, property, value);
        }
    }

    public static void SetProperties(this object obj, dynamic data)
        => obj.SetProperties(((object) data).ToReadonlyDictionary());
}
