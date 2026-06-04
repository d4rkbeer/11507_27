using System;
using System.ComponentModel.DataAnnotations;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "UserEmail is required")]
    public string? UserEmail { get; set; }

    [Required(ErrorMessage = "TotalAmount is required")]
    public decimal TotalAmount { get; set; }

    public string? Status { get; set; } = "Created";
}