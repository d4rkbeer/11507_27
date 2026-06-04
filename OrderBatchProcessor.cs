using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OrderBatchProcessor
{
    private readonly OrderProcessor _orderProcessor = new OrderProcessor();
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(5); 

    public async Task ProcessBatchAsync(List<Order> orders)
    {
        var tasks = orders.Select(async order =>
        {
            await _semaphore.WaitAsync();
            try
            {
                _orderProcessor.ProcessOrder(order);
            }
            finally
            {
                _semaphore.Release();
            }
        });

        await Task.WhenAll(tasks);
    }
}