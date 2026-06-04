using System;
using System.Collections.Generic;

class Program
{
    static async Task Main()
    {
        var orders = new List<Order>
        {
            new Order { UserEmail = "user1@example.com", TotalAmount = 1500, Status = "Paid" },
            new Order { UserEmail = "user2@example.com", TotalAmount = 800, Status = "Paid" },
            new Order { UserEmail = null, TotalAmount = 2000, Status = "Paid" }, // Невалидный заказ
            new Order { UserEmail = "user4@example.com", TotalAmount = 500, Status = "Created" }, // Не оплачен
        };

        // Настраиваем логирование
        var processor = new OrderProcessor();
        var logger = new AuditLogger();
        logger.Subscribe(processor);

        // Обрабатываем пачку заказов
        var batchProcessor = new OrderBatchProcessor();
        await batchProcessor.ProcessBatchAsync(orders);

        Console.WriteLine("Batch processing completed.");
    }
}