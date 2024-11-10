using System;
using System.Collections.Generic;

namespace KPZ_lab5.Models;

public partial class Counterparty
{
    public string Name { get; set; } = null!;

    public string TaxId { get; set; } = null!;

    public string? Address { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<NewInvoice> NewInvoices { get; set; } = new List<NewInvoice>();
}
