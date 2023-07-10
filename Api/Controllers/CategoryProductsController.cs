using Dtat.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class CategoryProductsController : Utilities.ControllerBaseTemp
    {
        /// <param name="databaseContext"></param>
		public CategoryProductsController
            (Persistence.DatabaseContext databaseContext, MediatR.IMediator mediator) : base(databaseContext: databaseContext)
        {
            Mediator = mediator;
        }

        #region Properties
        protected MediatR.IMediator Mediator { get; init; }

        #endregion /Properties

        #region PostAsync
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(Dtat.Results.Result
                <ViewModels.CategoryProducts.CategoryProductViewModel>),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public
            async
            System.Threading.Tasks.Task
            <Dtat.Results.Result<ViewModels.CategoryProducts.CategoryProductViewModel>>
            PostAsync
            ([Microsoft.AspNetCore.Mvc.FromBody]
            ViewModels.CategoryProducts.CategoryProductRequestViewModel viewModel)
        {
            var result =
                new FluentResults.Result
                <ViewModels.CategoryProducts.CategoryProductViewModel>();

            try
            {
                // **************************************************
                var categoryProductResult =
                    Domain.Aggregates.CategoryProducts.CategoryProduct.Create(name: viewModel.Name);

                if (categoryProductResult.IsFailed)
                {
                    result.WithErrors(errors: categoryProductResult.Errors);

                    return result.ConvertToDtatResult();
                }
                // **************************************************

                // **************************************************
                // نکته مهم: دستور ذیل کار نمی‌کند
                //bool hasAny =
                //	DatabaseContext
                //	.Provinces
                //	.Where(current => current.Name.Value.ToLower()
                //		== categoryProductResult.Value.Name.Value.ToLower())
                //	.Any();

                bool hasAny =
                    DatabaseContext
                    .CategoryProducts
                    .Where(current => current.Name == categoryProductResult.Value.Name)
                    .Any();

                if (hasAny)
                {
                    string errorMessage = string.Format
                        (Resources.Messages.Validations.Repetitive,
                        Resources.DataDictionary.CategoryProduct);

                    result.WithError(errorMessage: errorMessage);

                    return result.ConvertToDtatResult();
                }
                // **************************************************

                // **************************************************
                // دستور ذیل کار نمی‌کند DDD با نگاه
                //DatabaseContext.Provinces.Add(categoryProductResult.Value);

                var entity =
                    DatabaseContext.Attach(categoryProductResult.Value);

                entity.State =
                    Microsoft.EntityFrameworkCore.EntityState.Added;

                await DatabaseContext.SaveChangesAsync();
                // **************************************************

                // **************************************************
                var value =
                    new ViewModels.CategoryProducts.CategoryProductViewModel
                    {
                        Id = categoryProductResult.Value.Id,
                        Name = categoryProductResult.Value.Name.Value,
                    };

                result.WithValue(value: value);
                // **************************************************

                // **************************************************
                string successMessage = string.Format
                    (Resources.Messages.Successes.SuccessCreate,
                    Resources.DataDictionary.CategoryProduct);

                result.WithSuccess(successMessage: successMessage);
                // **************************************************
            }
            catch //System.Exception ex)
            {
                // Log Error!

                result.WithError
                    (errorMessage: Resources.Messages.Errors.UnexpectedError);
            }

            return result.ConvertToDtatResult();
        }
        #endregion /PostAsync

        #region /Get by Dapper and CQRS

        #region GetByDapperQueryAsync
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[Microsoft.AspNetCore.Mvc.HttpGet]

        [Microsoft.AspNetCore.Mvc.Consumes
         (contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

        [Microsoft.AspNetCore.Mvc.Produces
         (contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
         (type: typeof(System.Collections.Generic.IEnumerable
         <Domain.Aggregates.CategoryProducts.CategoryProduct>),
         statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
         (statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

        public async System.Threading.Tasks.Task
         <Microsoft.AspNetCore.Mvc.IActionResult>
            GetByDapperQueryAsync()
        {

            var request = new Api.CategoryProducts.Queries
                .GetCategoryProductQuery();


            var result =
                await
                Mediator.Send(request: request);

            return Ok(value: result);

        }
        #endregion GetByDapperQueryAsync

        #endregion /Get by Dapper and CQRS

        #region GetAsync
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(Dtat.Results.Result
                <System.Collections.Generic.IList
                <ViewModels.CategoryProducts.CategoryProductViewModel>>),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public
            async
            System.Threading.Tasks.Task
            <Dtat.Results.Result
                <System.Collections.Generic.IList
                <ViewModels.CategoryProducts.CategoryProductViewModel>>>
            GetAsync()
        {
            var result =
                new FluentResults.Result
                <System.Collections.Generic.IList
                <ViewModels.CategoryProducts.CategoryProductViewModel>>();

            try
            {
                var value =
                    await
                    DatabaseContext.CategoryProducts
                    .Select(current => new ViewModels.CategoryProducts.CategoryProductViewModel
                    {
                        Id = current.Id,
                        Name = current.Name.Value,
                    })
                    .ToListAsync()
                    ;

                result.WithValue(value: value);
            }
            catch //(System.Exception ex)
            {
                // Log Error!

                result.WithError
                    (errorMessage: Resources.Messages.Errors.UnexpectedError);
            }

            return result.ConvertToDtatResult();
        }
        #endregion /GetAsync

        #region GetByIdAsync
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}")]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(Dtat.Results.Result
                <ViewModels.CategoryProducts.CategoryProductViewModel>),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public
            async
            System.Threading.Tasks.Task
            <Dtat.Results.Result<ViewModels.CategoryProducts.CategoryProductViewModel>>
            GetByIdAsync([Microsoft.AspNetCore.Mvc.FromRoute] System.Guid id)
        {
            var result =
                new FluentResults.Result
                <ViewModels.CategoryProducts.CategoryProductViewModel>();

            try
            {
                var value =
                    await
                    DatabaseContext.CategoryProducts
                    .Where(current => current.Id == id)
                    .Select(current => new ViewModels.CategoryProducts.CategoryProductViewModel
                    {
                        Id = current.Id,
                        Name = current.Name.Value,
                    })
                    .FirstOrDefaultAsync();

                if (value is null)
                {
                    string errorMessage = string.Format
                        (Resources.Messages.Validations.NotFound,
                        Resources.DataDictionary.CategoryProduct);

                    result.WithError(errorMessage: errorMessage);

                    return result.ConvertToDtatResult();
                }

                result.WithValue(value: value);
            }
            catch //(System.Exception ex)
            {
                // Log Error!

                result.WithError
                    (errorMessage: Resources.Messages.Errors.UnexpectedError);
            }

            return result.ConvertToDtatResult();
        }
        #endregion /GetByIdAsync

        #region PutAsync
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpPut]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(Dtat.Results.Result
                <ViewModels.CategoryProducts.CategoryProductViewModel>),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public
            async
            System.Threading.Tasks.Task
            <Dtat.Results.Result<ViewModels.CategoryProducts.CategoryProductViewModel>>
            PutAsync
            ([Microsoft.AspNetCore.Mvc.FromBody]
            ViewModels.CategoryProducts.CategoryProductViewModel viewModel)
        {
            var result =
                new FluentResults.Result
                <ViewModels.CategoryProducts.CategoryProductViewModel>();

            try
            {
                // **************************************************
                if (viewModel.Id is null)
                {
                    string errorMessage = string.Format
                        (Resources.Messages.Validations.Required,
                        Resources.DataDictionary.Id);

                    result.WithError(errorMessage: errorMessage);

                    return result.ConvertToDtatResult();
                }
                // **************************************************

                // **************************************************
                var foundedObject =
                    await
                    DatabaseContext.CategoryProducts
                    .Where(current => current.Id == viewModel.Id.Value)
                    .FirstOrDefaultAsync();

                if (foundedObject == null)
                {
                    string errorMessage = string.Format
                        (Resources.Messages.Validations.NotFound,
                        Resources.DataDictionary.CategoryProduct);

                    result.WithError(errorMessage: errorMessage);

                    return result.ConvertToDtatResult();
                }
                // **************************************************

                // **************************************************
                var categoryProductResult =
                    foundedObject.Update(name: viewModel.Name);

                if (categoryProductResult.IsFailed)
                {
                    result.WithErrors(errors: categoryProductResult.Errors);

                    return result.ConvertToDtatResult();
                }
                // **************************************************

                // **************************************************
                // نکته مهم: دستور ذیل کار نمی‌کند
                //bool hasAny =
                //	DatabaseContext
                //	.Provinces
                //	.Where(current => current.Id != foundedObject.Id)
                //	.Where(current => current.Name == foundedObject.Name)
                //	.Any();

                bool hasAny =
                    DatabaseContext
                    .CategoryProducts
                    .Where(current => current.Id != foundedObject.Id)
                    .Where(current => current.Name == foundedObject.Name)
                    .Any();

                if (hasAny)
                {
                    string errorMessage = string.Format
                        (Resources.Messages.Validations.Repetitive,
                        Resources.DataDictionary.CategoryProduct);

                    result.WithError(errorMessage: errorMessage);

                    return result.ConvertToDtatResult();
                }
                // **************************************************

                // **************************************************
                //var entity =
                //	DatabaseContext.Attach(foundedObject);

                //entity.State =
                //	Microsoft.EntityFrameworkCore.EntityState.Modified;

                await DatabaseContext.SaveChangesAsync();
                // **************************************************

                // **************************************************
                var value =
                    new ViewModels.CategoryProducts.CategoryProductViewModel
                    {
                        Id = foundedObject.Id,
                        Name = foundedObject.Name.Value,
                    };

                result.WithValue(value: value);
                // **************************************************

                // **************************************************
                string successMessage = string.Format
                    (Resources.Messages.Successes.SuccessUpdate,
                    Resources.DataDictionary.CategoryProduct);

                result.WithSuccess(successMessage: successMessage);
                // **************************************************
            }
            catch //(System.Exception ex)
            {
                // Log Error!

                result.WithError
                    (errorMessage: Resources.Messages.Errors.UnexpectedError);
            }

            return result.ConvertToDtatResult();
        }
        #endregion /PutAsync

        #region DeleteByIdAsync
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpDelete(template: "{id}")]

        [Microsoft.AspNetCore.Mvc.ProducesResponseType
            (type: typeof(Dtat.Results.Result),
            statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public
            async
            System.Threading.Tasks.Task<Dtat.Results.Result>
            DeleteByIdAsync([Microsoft.AspNetCore.Mvc.FromRoute] System.Guid id)
        {
            var result =
                new FluentResults.Result();

            try
            {
                var foundedObject =
                    await
                    DatabaseContext.CategoryProducts
                    .Where(current => current.Id == id)
                    .FirstOrDefaultAsync();

                if (foundedObject is null)
                {
                    string errorMessage = string.Format
                        (Resources.Messages.Validations.NotFound, Resources.DataDictionary.CategoryProduct);

                    result.WithError(errorMessage: errorMessage);

                    return result.ConvertToDtatResult();
                }

                DatabaseContext.Remove(foundedObject);

                //DatabaseContext.CategoryProducts.Remove(foundedObject);

                await DatabaseContext.SaveChangesAsync();

                // **************************************************
                string successMessage = string.Format
                    (Resources.Messages.Successes.SuccessDelete, Resources.DataDictionary.CategoryProduct);

                result.WithSuccess(successMessage: successMessage);
                // **************************************************
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Errors.CanNotDelete, Resources.DataDictionary.CategoryProduct);

                result.WithError
                    (errorMessage: errorMessage);
            }
            catch //(System.Exception ex)
            {
                // Log Error!

                string errorMessage =
                    Resources.Messages.Errors.UnexpectedError;

                result.WithError
                    (errorMessage: errorMessage);
            }

            return result.ConvertToDtatResult();
        }
        #endregion /DeleteByIdAsync
    }
}
