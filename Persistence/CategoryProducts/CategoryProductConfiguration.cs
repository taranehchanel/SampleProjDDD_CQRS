using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.CategoryProducts
{
    internal sealed class CategoryProductConfiguration : object,
        Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Aggregates.CategoryProducts.CategoryProduct>
    {

        public CategoryProductConfiguration() : base()
        {
        }

        public void Configure
            (Microsoft.EntityFrameworkCore.Metadata.Builders
            .EntityTypeBuilder<Domain.Aggregates.CategoryProducts.CategoryProduct> builder)
        {
            // **************************************************
            //builder
            //	// using Microsoft.EntityFrameworkCore;
            //	.ToTable(name: "CategoryProducts")
            //	;
            // **************************************************

            // **************************************************
            builder
                .Property(p => p.Name)
                .IsRequired(required: true)
                .HasMaxLength(maxLength: Domain.SharedKernel.Name.MaxLength)
                .HasConversion(p => p.Value,
                    p => Domain.SharedKernel.Name.Create(p).Value)
                ;

            // دستور ذیل کار می‌کند ولی در زمان اصلاح نام گروه خطا می‌دهد
            //builder
            //	.HasAlternateKey(current => new { current.Name });

            // دستور ذیل کار می‌کند ولی در زمان اصلاح نام گروه خطا می‌دهد
            //builder
            //	.HasAlternateKey(current => current.Name);

            // دستور ذیل کار می‌کند ولی در زمان اصلاح نام گروه خطا می‌دهد
            //builder
            //	.HasAlternateKey(propertyNames: "Name");

            // دستور ذیل کار می‌کند ولی در زمان اصلاح نام گروه خطا می‌دهد
            //builder
            //	.HasAlternateKey(propertyNames: nameof(Domain.SharedKernel.Name));

            builder
                .HasIndex(p => p.Name)
                .IsUnique(unique: true)
                ;
            // **************************************************

            // **************************************************
            builder
                .HasMany(current => current.Products)
                .WithOne(current => current.CategoryProduct)
                .IsRequired(required: true)
                .HasForeignKey
                    (nameof(Domain.Aggregates.CategoryProducts.CategoryProduct) + nameof(Domain.SeedWork.Entity.Id))
                .OnDelete(deleteBehavior: Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
                ;
            // **************************************************

            // **************************************************
            // Seed
            // **************************************************
            var categoryProduct =
                Domain.Aggregates.CategoryProducts.CategoryProduct.Create("Clothes");

            builder.HasData(categoryProduct.Value);

            categoryProduct =
                Domain.Aggregates.CategoryProducts.CategoryProduct.Create("Book");

            builder.HasData(categoryProduct.Value);
            // **************************************************
        }
    }
}
