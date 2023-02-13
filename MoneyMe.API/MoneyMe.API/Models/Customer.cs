using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoneyMe.API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } 
        public Decimal AmountRequired { get; set; }
        public int Term { get; set; }
        public string Title { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }

    public class CustomerUserConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("tblcustomers");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.CustomerId).HasColumnName("customerid");
            builder.Property(p => p.AmountRequired).HasColumnName("amountrequired");
            builder.Property(p => p.Term).HasColumnName("term");
            builder.Property(p => p.Title).HasColumnName("title");
            builder.Property(p => p.FirstName).HasColumnName("firstname");
            builder.Property(p => p.LastName).HasColumnName("lastname");
            builder.Property(p => p.DateOfBirth).HasColumnName("birthdate");
            builder.Property(p => p.Mobile).HasColumnName("mobile");
            builder.Property(p => p.Email).HasColumnName("email");

            builder.HasKey(p => p.Id).IsClustered();

            builder.Property(p => p.CustomerId).HasMaxLength(450).HasDefaultValueSql("NEWID()");
            builder.Property(p => p.AmountRequired).HasColumnType("numeric(18, 2)").HasDefaultValue("0.00");
            builder.Property(p => p.Term).HasColumnType("INT").HasDefaultValue(0);
            builder.Property(p => p.Title).HasColumnType("VARCHAR").HasMaxLength(10).HasDefaultValue("");
            builder.Property(p => p.FirstName).HasColumnType("VARCHAR").HasMaxLength(50).HasDefaultValue("");
            builder.Property(p => p.LastName).HasColumnType("VARCHAR").HasMaxLength(50).HasDefaultValue("");
            builder.Property(p => p.DateOfBirth).HasColumnType("SMALLDATETIME").HasDefaultValueSql("01/01/1900");
            builder.Property(p => p.Mobile).HasColumnType("VARCHAR").HasMaxLength(20).HasDefaultValue("");
            builder.Property(p => p.Email).HasColumnType("VARCHAR").HasMaxLength(60).HasDefaultValue("");

            builder.HasIndex(p => p.CustomerId).IsUnique();

        }
    }
}
