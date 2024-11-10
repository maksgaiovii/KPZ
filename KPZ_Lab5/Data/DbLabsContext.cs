using System;
using System.Collections.Generic;
using KPZ_lab5.Models;
using Microsoft.EntityFrameworkCore;

namespace KPZ_lab5.Data;

public partial class DbLabsContext : DbContext
{
    public DbLabsContext()
    {
    }

    public DbLabsContext(DbContextOptions<DbLabsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountActivityReport> AccountActivityReports { get; set; }

    public virtual DbSet<Counterparty> Counterparties { get; set; }

    public virtual DbSet<DetailedAnalyticalReport> DetailedAnalyticalReports { get; set; }

    public virtual DbSet<ExistingTable> ExistingTables { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Invoicecategory> Invoicecategories { get; set; }

    public virtual DbSet<NewInvoice> NewInvoices { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Add your configuration code here, for example:
        // optionsBuilder.UseSqlServer("YourConnectionString");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("account_pkey");

            entity.ToTable("account");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.AccountName)
                .HasMaxLength(50)
                .HasColumnName("account_name");
            entity.Property(e => e.Currency)
                .HasMaxLength(4)
                .HasDefaultValueSql("'USD'::character varying")
                .HasColumnName("currency");
        });

        modelBuilder.Entity<AccountActivityReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("account_activity_report");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.ActivityType).HasColumnName("activity_type");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.EntityName)
                .HasColumnType("character varying")
                .HasColumnName("entity_name");
            entity.Property(e => e.EntityType).HasColumnName("entity_type");
            entity.Property(e => e.RankByAmount).HasColumnName("rank_by_amount");
            entity.Property(e => e.TotalAmountPerAccount).HasColumnName("total_amount_per_account");
        });

        modelBuilder.Entity<Counterparty>(entity =>
        {
            entity.HasKey(e => e.TaxId).HasName("counterparty_pkey");

            entity.ToTable("counterparty");

            entity.Property(e => e.TaxId)
                .HasMaxLength(15)
                .HasColumnName("tax_id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DetailedAnalyticalReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("detailed_analytical_report");

            entity.Property(e => e.AccountName)
                .HasMaxLength(50)
                .HasColumnName("account_name");
            entity.Property(e => e.AveragePayment).HasColumnName("average_payment");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
            entity.Property(e => e.CounterpartyName)
                .HasMaxLength(50)
                .HasColumnName("counterparty_name");
            entity.Property(e => e.PaymentRank).HasColumnName("payment_rank");
            entity.Property(e => e.TaxId)
                .HasMaxLength(15)
                .HasColumnName("tax_id");
            entity.Property(e => e.TotalInvoices).HasColumnName("total_invoices");
            entity.Property(e => e.TotalPayments).HasColumnName("total_payments");
            entity.Property(e => e.TotalPaymentsPerCounterparty).HasColumnName("total_payments_per_counterparty");
        });

        modelBuilder.Entity<ExistingTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("existing_table");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CounterpartyId)
                .HasMaxLength(15)
                .HasColumnName("counterparty_id");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.InvoiceCategoryId).HasColumnName("invoice_category_id");
            entity.Property(e => e.InvoiceDate).HasColumnName("invoice_date");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(15, 2)
                .HasColumnName("total_amount");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("invoice_pkey");

            entity.ToTable("invoice");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.AccountId)
                .ValueGeneratedOnAdd()
                .HasColumnName("account_id");
            entity.Property(e => e.CounterpartyId)
                .HasMaxLength(15)
                .HasColumnName("counterparty_id");
            entity.Property(e => e.DueDate)
                .HasDefaultValueSql("(CURRENT_DATE + '30 days'::interval)")
                .HasColumnName("due_date");
            entity.Property(e => e.InvoiceCategoryId).HasColumnName("invoice_category_id");
            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("invoice_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'unpaid'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(15, 2)
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Account).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("invoice_account_id_fkey");

            entity.HasOne(d => d.Counterparty).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CounterpartyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("invoice_counterparty_id_fkey");

            entity.HasOne(d => d.InvoiceCategory).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.InvoiceCategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("invoice_invoice_category_id_fkey");
        });

        modelBuilder.Entity<Invoicecategory>(entity =>
        {
            entity.HasKey(e => e.InvoiceCategoryId).HasName("invoicecategory_pkey");

            entity.ToTable("invoicecategory");

            entity.Property(e => e.InvoiceCategoryId).HasColumnName("invoice_category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasDefaultValueSql("'expense'::character varying")
                .HasColumnName("type");
        });

        modelBuilder.Entity<NewInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("new_invoice_pkey");

            entity.ToTable("new_invoice");

            entity.Property(e => e.InvoiceId)
                .ValueGeneratedNever()
                .HasColumnName("invoice_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CounterpartyId)
                .HasMaxLength(15)
                .HasColumnName("counterparty_id");
            entity.Property(e => e.DueDate)
                .HasDefaultValueSql("(CURRENT_DATE + '30 days'::interval)")
                .HasColumnName("due_date");
            entity.Property(e => e.InvoiceCategoryId).HasColumnName("invoice_category_id");
            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("invoice_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'unpaid'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Account).WithMany(p => p.NewInvoices)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("new_invoice_account_id_fkey");

            entity.HasOne(d => d.Counterparty).WithMany(p => p.NewInvoices)
                .HasForeignKey(d => d.CounterpartyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("new_invoice_counterparty_id_fkey");

            entity.HasOne(d => d.InvoiceCategory).WithMany(p => p.NewInvoices)
                .HasForeignKey(d => d.InvoiceCategoryId)
                .HasConstraintName("new_invoice_invoice_category_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("payment_pkey");

            entity.ToTable("payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.PaymentAmount)
                .HasPrecision(15, 2)
                .HasColumnName("payment_amount");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("payment_date");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("payment_invoice_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
