using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.CategoryProducts
{
    public class CategoryProduct : SeedWork.AggregateRoot
    {
        #region Static Member(s)
        public static FluentResults.Result<CategoryProduct> Create(string name)
        {
            var result =
                new FluentResults.Result<CategoryProduct>();

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
                new CategoryProduct(name: nameResult.Value);

            result.WithValue(value: returnValue);

            return result;
        }
        #endregion /Static Member(s)

        private CategoryProduct() : base()
        {
            _products =
                new System.Collections.Generic.List<Products.Product>();
        }

        private CategoryProduct(SharedKernel.Name name) : this()
        {
            Name = name;
        }

        public SharedKernel.Name Name { get; private set; }

        // **********
        private readonly System.Collections.Generic.List<Products.Product> _products;

        public virtual System.Collections.Generic.IReadOnlyList<Products.Product> Products
        {
            get
            {
                return _products;
            }
        }
        // **********

        public FluentResults.Result Update(string name)
        {
            var result =
                Create(name: name);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            Name = result.Value.Name;

            return result.ToResult();
        }

        public FluentResults.Result<Products.Product> AddCity(string cityName)
        {
            var result =
                new FluentResults.Result<Products.Product>();

            // **************************************************
            var cityResult =
                Aggregates.Products.Product.Create(categoryProduct: this, name: cityName);

            if (cityResult.IsFailed)
            {
                result.WithErrors(errors: cityResult.Errors);

                return result;
            }
            // **************************************************

            // **************************************************
            var hasAny =
                _products
                .Where(current => current.Name.Value.ToLower()
                    == cityResult.Value.Name.Value.ToLower())
                .Any();

            if (hasAny)
            {
                string errorMessage = 
                    string.Format
                    (Resources.Messages.Validations.Repetitive,
                    Resources.DataDictionary.CityName);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            _products.Add(cityResult.Value);

            result.WithValue(cityResult.Value);

            return result;
        }

        public FluentResults.Result RemoveCity(string cityName)
        {
            var result =
                new FluentResults.Result();

            // **************************************************
            var cityResult =
                Aggregates.Products.Product.Create(categoryProduct: this, name: cityName);

            if (cityResult.IsFailed)
            {
                result.WithErrors(errors: cityResult.Errors);

                return result;
            }
            // **************************************************

            // **************************************************
            var foundedCity =
                _products
                .Where(current => current.Name.Value.ToLower() == cityResult.Value.Name.Value.ToLower())
                .FirstOrDefault();

            if (foundedCity == null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.City);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            _products.Remove(foundedCity);

            return result;
        }
    }
}
