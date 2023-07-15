namespace Domain.Aggregates.CategoryProducts
{
    public interface ICategoryProductRepository
    {
        // حالت عادی و سنتی  به این روش مینوشتیم

        //CategoryProduct GetById(System.Guid id);
        //System.Collections.Generic.IEnumerable<CategoryProduct> GetAll();

        System.Threading.Tasks.Task<CategoryProduct> GetByIdAsync
            (System.Guid id, System.Threading.CancellationToken cancellationToken = default);


        System.Threading.Tasks.Task<System.Collections.Generic.IList<ViewModels.CategoryProducts.CategoryProductViewModel>>
            GetAllAsync(System.Threading.CancellationToken cancellationToken = default);
    }


}
