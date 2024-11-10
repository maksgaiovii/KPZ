using System;
using System.Collections.Generic;

namespace KPZ_lab5.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string AccountName { get; set; } = null!;

    public string Currency { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<NewInvoice> NewInvoices { get; set; } = new List<NewInvoice>();
}
