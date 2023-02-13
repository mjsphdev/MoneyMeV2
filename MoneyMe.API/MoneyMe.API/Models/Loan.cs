using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MoneyMe.API.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public string CustomerId { get; set; }
        public Decimal TotalRepayment { get; set; }
        public Decimal TotalInterest { get; set; }
        public Decimal EstablishmentFee { get; set; }
        public Decimal RepaymentsFrom { get; set; }
    }

    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("tblloans");
            builder.Property(p => p.LoanId).HasColumnName("id");
            builder.Property(p => p.CustomerId).HasColumnName("customerid");
            builder.Property(p => p.TotalRepayment).HasColumnName("totalrepayment");
            builder.Property(p => p.TotalInterest).HasColumnName("totalinterest");
            builder.Property(p => p.EstablishmentFee).HasColumnName("establishmentfee");
            builder.Property(p => p.RepaymentsFrom).HasColumnName("repaymentsfrom");

            builder.HasKey(p => p.LoanId).IsClustered();

            builder.Property(p => p.TotalRepayment).HasColumnType("numeric(18, 2)").HasDefaultValue("0.00");
            builder.Property(p => p.TotalInterest).HasColumnType("numeric(18, 2)").HasDefaultValue("0.00");
            builder.Property(p => p.EstablishmentFee).HasColumnType("numeric(18, 2)").HasDefaultValue("0.00");
            builder.Property(p => p.RepaymentsFrom).HasColumnType("numeric(18, 2)").HasDefaultValue("0.00");
        }
    }
}
