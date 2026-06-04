using System;
using System.Threading.Tasks;

public static class DeliveryService
{
    private static readonly Random _random = new Random();

    public static async Task<bool> SendToDeliveryAsync(Order order)
    {
        await Task.Delay(500);

        // Проверка статуса
        if (order.Status != "Paid")
        {
            throw new InvalidOperationException("Order is not paid");
        }

        // Cлучайная ошибка
        if (_random.Next(1, 101) == 1)
        {
            throw new Exception("External service error");
        }

        return true;
    }
}