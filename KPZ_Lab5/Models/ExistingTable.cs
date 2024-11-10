using System;
using System.Collections.Generic;

namespace KPZ_lab5.Models;

public partial class ExistingTable
{
    public int? InvoiceId { get; set; }

    public string? CounterpartyId { get; set; }

    public int? AccountId { get; set; }

    public DateOnly? InvoiceDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }

    public int? InvoiceCategoryId { get; set; }
}
