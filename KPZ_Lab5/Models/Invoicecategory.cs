using System;
using System.Collections.Generic;

namespace KPZ_lab5.Models;

public partial class Invoicecategory
{
    public int InvoiceCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string Type { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<NewInvoice> NewInvoices { get; set; } = new List<NewInvoice>();
}
