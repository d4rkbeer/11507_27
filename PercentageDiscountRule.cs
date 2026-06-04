public class PercentageDiscountRule : IDiscountRule
{
    public void Apply(Order order)
    {
        if (order.TotalAmount > 1000)
        {
            order.TotalAmount *= 0.9m;
        }
    }
}