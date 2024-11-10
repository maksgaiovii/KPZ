using System;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace KPZ_lab5.ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        [SwaggerSchema("The name of the account")]
        public string Name { get; set; }

        [Required, MaxLength(4)]
        [SwaggerSchema("The currency code associated with the account")]
        public string Currency { get; set; }
    }

    public class CounterpartyViewModel
    {
        public string Id { get; set; } // TaxId has been renamed to Id

        [Required, MaxLength(50)]
        [SwaggerSchema("The name of the counterparty")]
        public string Name { get; set; }

        [MaxLength(150)]
        [SwaggerSchema("The address of the counterparty")]
        public string Address { get; set; }

        [MaxLength(50)]
        [SwaggerSchema("The email of the counterparty")]
        public string Email { get; set; }
    }

    public class InvoiceViewModel
    {
        public int Id { get; set; } // InvoiceId has been renamed to Id

        [Required]
        [SwaggerSchema("Identifier for the associated account")]
        public int AccountId { get; set; }

        [Required, MaxLength(15)]
        [SwaggerSchema("Identifier for the associated counterparty")]
        public string CounterpartyId { get; set; }

        [SwaggerSchema("The date when the invoice was created")]
        public DateTime InvoiceDate { get; set; }

        [SwaggerSchema("The due date for the invoice")]
        public DateTime DueDate { get; set; }

        [Range(0, double.MaxValue)]
        [SwaggerSchema("Total amount of the invoice")]
        public decimal TotalAmount { get; set; }

        [Required, AllowedValues(
             ["Paid", "Unpaid", "Overdue"]
             )
        ]
        [SwaggerSchema("Current status of the invoice (e.g., 'Paid', 'Unpaid', 'Overdue')")]
        public string Status { get; set; }

        [SwaggerSchema("Identifier for the category of the invoice")]
        public int CategoryId { get; set; }
    }

    public class PaymentViewModel
    {
        public int Id { get; set; } // PaymentId has been renamed to Id

        [Required]
        [SwaggerSchema("Identifier for the associated invoice")]
        public int InvoiceId { get; set; }

        [SwaggerSchema("The date when the payment was made")]
        public DateTime Date { get; set; } // Changed to "Date"

        [Range(0, double.MaxValue)]
        [SwaggerSchema("Amount of the payment")]
        public decimal Amount { get; set; }
    }

    public class InvoiceCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } // Changed to "Name"
        public string Description { get; set; }
    }
}
