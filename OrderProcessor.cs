using System;
using System.Reflection;

public class OrderProcessor
{
    public event Action<Guid, string>? OnOrderStateChanged;

    public void ProcessOrder(Order order)
    {
        if (!OrderValidator.Validate(order, out var errorMessage))
        {
            OnOrderStateChanged?.Invoke(order.Id, $"Validation failed: {errorMessage}");
            return;
        }

        ApplyDiscounts(order);

        try
        {
            DeliveryService.SendToDeliveryAsync(order).Wait();
            OnOrderStateChanged?.Invoke(order.Id, "Sent to delivery");
        }
        catch (Exception ex)
        {
            OnOrderStateChanged?.Invoke(order.Id, $"Delivery failed: {ex.Message}");
        }
    }

    private void ApplyDiscounts(Order order)
    {
        var discountRules = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass && typeof(IDiscountRule).IsAssignableFrom(t))
            .Select(t => (IDiscountRule)Activator.CreateInstance(t)!)
            .ToList();

        foreach (var rule in discountRules)
        {
            rule.Apply(order);
        }
    }
}