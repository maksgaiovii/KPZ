using KPZ_lab5.Models;
using Microsoft.EntityFrameworkCore;

namespace KPZ_lab5.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        // DbSet для нових моделей
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Counterparty> Counterparties { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceCategory> InvoiceCategories { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Встановлення первинних ключів та послідовностей
            modelBuilder.Entity<Account>()
                .Property(a => a.AccountId)
                .HasDefaultValueSql("nextval('public.account_account_id_seq')");

            modelBuilder.Entity<Invoice>()
                .Property(i => i.InvoiceId)
                .HasDefaultValueSql("nextval('public.invoice_invoice_id_seq')");

            modelBuilder.Entity<InvoiceCategory>()
                .Property(ic => ic.InvoiceCategoryId)
                .HasDefaultValueSql("nextval('public.invoicecategory_invoice_category_id_seq')");

            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentId)
                .HasDefaultValueSql("nextval('public.payment_payment_id_seq')");

            // Налаштування зв’язків між моделями
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Account)
                .WithMany(a => a.Invoices)
                .HasForeignKey(i => i.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Counterparty)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CounterpartyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.InvoiceCategory)
                .WithMany(ic => ic.Invoices)
                .HasForeignKey(i => i.InvoiceCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Invoice)
                .WithMany(i => i.Payments)
                .HasForeignKey(p => p.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Домени для специфічних типів даних
            modelBuilder.Entity<Invoice>()
                .Property(i => i.Status)
                .HasColumnType("public.invoice_status")
                .HasDefaultValue(InvoiceStatus.Unpaid);

            modelBuilder.Entity<InvoiceCategory>()
                .Property(ic => ic.Type)
                .HasColumnType("public.category_type")
                .HasDefaultValue(CategoryType.Expense);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.TotalAmount)
                .HasColumnType("public.positive_amount")
                .HasDefaultValue(0.00);

            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentAmount)
                .HasColumnType("public.positive_amount")
                .HasDefaultValue(0.00);
        }
    }
}
