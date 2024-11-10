using System;
using System.Collections.Generic;

namespace KPZ_lab5.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int InvoiceId { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal PaymentAmount { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
