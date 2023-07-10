using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Products
{
    public class Product : SeedWork.AggregateRoot
    {
        #region Static Member(s)
        public static FluentResults.Result<Product>
            Create(CategoryProducts.CategoryProduct categoryProduct, string name)
        {
            var result =
                new FluentResults.Result<Product>();

            // **************************************************
            if (categoryProduct is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required,
                    Resources.DataDictionary.Province);

                result.WithError(errorMessage: errorMessage);
            }
            // **************************************************

            // **************************************************
            var nameResult =
                SharedKernel.Name.Create(value: name);

            result.WithErrors(errors: nameResult.Errors);
            // **************************************************

            if (result.IsFailed)
            {
                return result;
            }

            var returnValue =
                new Product(categoryProduct: categoryProduct, name: nameResult.Value);

            result.WithValue(value: returnValue);

            return result;
        }
        #endregion /Static Member(s)

        private Product() : base()
        {
        }

        private Product(CategoryProducts.CategoryProduct categoryProduct, SharedKernel.Name name) : this()
        {
            Name = name;
            CategoryProduct = categoryProduct;
        }

        public SharedKernel.Name Name { get; private set; }

        public virtual CategoryProducts.CategoryProduct CategoryProduct { get; private set; }

        public FluentResults.Result Update(string name)
        {
            var result =
                Create(categoryProduct: CategoryProduct, name: name);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            Name = result.Value.Name;

            return result.ToResult();
        }
    }
}
