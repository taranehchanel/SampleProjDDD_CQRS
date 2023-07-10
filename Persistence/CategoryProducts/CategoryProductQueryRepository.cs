using Dapper;
using Microsoft.Extensions.Configuration;


namespace Persistence.CategoryProducts
{
    public class CategoryProductQueryRepository : object,
    Domain.Aggregates.CategoryProducts.ICategoryProductRepository
    {
        public CategoryProductQueryRepository
            (Microsoft.Extensions.Configuration.IConfiguration configuration) : base()
        {
            // using Microsoft.Extensions.Configuration;
            ConnectionString =
                configuration.GetConnectionString(name: "DatabaseContext");
        }

        protected string? ConnectionString { get; init; }

        public async System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable
            <Domain.Aggregates.CategoryProducts.CategoryProduct>> GetAllAsync
            (System.Threading.CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(value: ConnectionString))
            {
                throw new System.ArgumentNullException
                    (paramName: nameof(ConnectionString));
            }

            using var connection = new Microsoft.Data.SqlClient
                .SqlConnection(connectionString: ConnectionString);

            var query =
                "SELECT * FROM CategoryProducts";

            // using Dapper;
            var result =
                await
                connection.QueryAsync<Domain.Aggregates.CategoryProducts.CategoryProduct>(sql: query)
                ;

            return result;
        }

        public async System.Threading.Tasks.Task<Domain.Aggregates.CategoryProducts.CategoryProduct>
            GetByIdAsync(System.Guid id, System.Threading.CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(value: ConnectionString))
            {
                throw new System.ArgumentNullException
                    (paramName: nameof(ConnectionString));
            }

            using var connection = new Microsoft.Data.SqlClient
                .SqlConnection(connectionString: ConnectionString);

            var query =
                "SELECT * FROM CategoryProducts WHERE Id = @Id";

            //var parameters =
            //	new { Id = id };

            var parameters = new { id };

            // using Dapper;
            var result =
                await
                connection.QueryFirstOrDefaultAsync<Domain.Aggregates.CategoryProducts.CategoryProduct>
                (sql: query, param: parameters);

            return result;
        }
    }
}
