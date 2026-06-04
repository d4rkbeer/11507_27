using System;

public class AuditLogger
{
    public void Subscribe(OrderProcessor processor)
    {
        processor.OnOrderStateChanged += (orderId, stage) =>
        {
            Console.WriteLine($"Order {orderId}: {stage}");
        }
        ;

    }
}