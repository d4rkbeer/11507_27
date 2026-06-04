using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public static class OrderValidator
{
    public static bool Validate(Order order, out string? errorMessage)
    {
        errorMessage = null;
        var properties = order.GetType().GetProperties();

        foreach (var property in properties)
        {
            var requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();
            if (requiredAttribute != null)
            {
                var value = property.GetValue(order);
                if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                {
                    errorMessage = $"{property.Name} is required";
                    return false;
                }
            }
        }

      
        return true;
    }



}
