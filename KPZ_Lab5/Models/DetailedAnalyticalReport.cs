using System;
using System.Collections.Generic;

namespace KPZ_lab5.Models;

public partial class DetailedAnalyticalReport
{
    public string? TaxId { get; set; }

    public string? CounterpartyName { get; set; }

    public string? AccountName { get; set; }

    public string? CategoryName { get; set; }

    public long? TotalInvoices { get; set; }

    public decimal? TotalPayments { get; set; }

    public decimal? AveragePayment { get; set; }

    public decimal? TotalPaymentsPerCounterparty { get; set; }

    public long? PaymentRank { get; set; }
}
