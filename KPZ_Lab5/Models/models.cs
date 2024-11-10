using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPZ_lab5.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required, MaxLength(50)]
        public string AccountName { get; set; }

        [Required, MaxLength(4)]
        public string Currency { get; set; } = "USD";

        // Navigation Properties
        public ICollection<Invoice> Invoices { get; set; }
    }

    public class Counterparty
    {
        [Key, MaxLength(15)]
        public string TaxId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        // Navigation Properties
        public ICollection<Invoice> Invoices { get; set; }
    }

    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required, MaxLength(15)]
        public string CounterpartyId { get; set; }

        public DateTime InvoiceDate { get; set; } = DateTime.Today;

        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(30);

        [Column(TypeName = "numeric(15, 2)")]
        public decimal TotalAmount { get; set; } = 0.00M;

        [Required]
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Unpaid;

        public int InvoiceCategoryId { get; set; }

        // Navigation Properties
        public Account Account { get; set; }
        public Counterparty Counterparty { get; set; }
        public InvoiceCategory InvoiceCategory { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }

    public class InvoiceCategory
    {
        [Key]
        public int InvoiceCategoryId { get; set; }

        [Required, MaxLength(100)]
        public string CategoryName { get; set; }

        [Required]
        public CategoryType Type { get; set; } = CategoryType.Expense;

        // Navigation Properties
        public ICollection<Invoice> Invoices { get; set; }
    }

    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Today;

        [Column(TypeName = "numeric(15, 2)")]
        public decimal PaymentAmount { get; set; } = 0.00M;

        // Navigation Properties
        public Invoice Invoice { get; set; }
    }

    public enum InvoiceStatus
    {
        Paid,
        Unpaid,
        Overdue
    }

    public enum CategoryType
    {
        Expense,
        Income
    }
}
