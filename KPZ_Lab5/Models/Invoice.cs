﻿using System;
using System.Collections.Generic;

namespace KPZ_lab5.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public string? CounterpartyId { get; set; }

    public int AccountId { get; set; }

    public DateOnly InvoiceDate { get; set; }

    public DateOnly DueDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public int? InvoiceCategoryId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Counterparty? Counterparty { get; set; }

    public virtual Invoicecategory? InvoiceCategory { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

