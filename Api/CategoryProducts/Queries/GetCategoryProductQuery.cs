namespace Api.CategoryProducts.Queries;

public record GetCategoryProductQuery() : object, MediatR.IRequest
<System.Collections.Generic.IEnumerable<Domain.Aggregates.CategoryProducts.CategoryProduct>>;
