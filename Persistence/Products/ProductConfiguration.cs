using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Products
{
    internal sealed class ProductConfiguration : object,
                Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Aggregates.Products.Product>

    {
        public ProductConfiguration() : base()
        {
        }

        public void Configure
            (Microsoft.EntityFrameworkCore.Metadata.Builders
            .EntityTypeBuilder<Domain.Aggregates.Products.Product> builder)
        {
            // **************************************************
            //builder
            //	// using Microsoft.EntityFrameworkCore;
            //	.ToTable(name: "Products")
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


            builder
                .HasIndex
                    (nameof(Domain.Aggregates.CategoryProducts.CategoryProduct) + nameof(Domain.SeedWork.Entity.Id),
                    nameof(Domain.Aggregates.CategoryProducts.CategoryProduct.Name))
                .IsUnique(unique: true)
                ;
            // **************************************************
        }
    }
}
