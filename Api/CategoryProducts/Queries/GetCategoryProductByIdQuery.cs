// **************************************************
// دستور اول هم درست است ولی جهت استفاده از
//CQRS
// از دستور بعدی استفاده کردیم و این کامنت شده

//namespace Api.CategoryProducts.Queries;

//public class GetCategoryProductByIdQuery : object,
//	MediatR.IRequest<Domain.Aggregates.CategoryProducts.CategoryProduct>
//{
//	public GetCategoryProductByIdQuery(System.Guid id) : base()
//	{
//		Id = id;
//	}

//	public System.Guid Id { get; init; }
//}
// **************************************************

// **************************************************
namespace Api.CategoryProducts.Queries;

public record GetCategoryProductByIdQuery(System.Guid Id) : object,
    MediatR.IRequest<Domain.Aggregates.CategoryProducts.CategoryProduct>;
// **************************************************


