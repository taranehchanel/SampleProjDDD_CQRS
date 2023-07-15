
// ****************************** whithout Dapper works OK too  ********************

//using Microsoft.EntityFrameworkCore;

//namespace Api.CategoryProducts.Queries;

//public class GetCategoryProductsHandler : object, MediatR.IRequestHandler<GetCategoryProductQuery,
//    System.Collections.Generic.IEnumerable<Domain.Aggregates.CategoryProducts.CategoryProduct>>
//{
//    public GetCategoryProductsHandler
//        (Persistence.DatabaseContext databaseContext) : base()
//    {
//        DatabaseContext = databaseContext;
//    }

//    protected Persistence.DatabaseContext DatabaseContext { get; init; }

//    public async System.Threading.Tasks.Task
//        <System.Collections.Generic.IEnumerable
//        <Domain.Aggregates.CategoryProducts.CategoryProduct>> Handle(GetCategoryProductQuery request,
//        System.Threading.CancellationToken cancellationToken = default)
//    {
//        var result =
//            await
//            DatabaseContext.CategoryProducts
//            .ToListAsync()
//            ;

//        return result;
//    }
//}
// **************************************************

// ********************** by Dapper ****************************

namespace Api.CategoryProducts.Queries;
public class GetCategoryProductsHandler : object, MediatR.IRequestHandler<GetCategoryProductQuery,
    System.Collections.Generic.IList<ViewModels.CategoryProducts.CategoryProductViewModel>>
{
    public GetCategoryProductsHandler(Domain.Aggregates.CategoryProducts
        .ICategoryProductRepository categoryProductQueryRepository) : base()

    {
        CategoryProductQueryRepository = categoryProductQueryRepository;
    }

    protected Domain.Aggregates.CategoryProducts.ICategoryProductRepository CategoryProductQueryRepository { get; init; }

    public async System.Threading.Tasks.Task
        <System.Collections.Generic.IList
        <ViewModels.CategoryProducts.CategoryProductViewModel>> Handle(GetCategoryProductQuery request,
        System.Threading.CancellationToken cancellationToken = default)
    {
        var result =
            await
            CategoryProductQueryRepository.GetAllAsync();

        return result;
    }
}
// **************************************************
