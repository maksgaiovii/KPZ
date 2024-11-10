using System;
using System.Collections.Generic;

namespace KPZ_lab5.Models;

public partial class AccountActivityReport
{
    public int? AccountId { get; set; }

    public string? EntityName { get; set; }

    public string? EntityType { get; set; }

    public decimal? Amount { get; set; }

    public string? ActivityType { get; set; }

    public decimal? TotalAmountPerAccount { get; set; }

    public long? RankByAmount { get; set; }
}
