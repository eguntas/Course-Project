using Course.Shared;
using MediatR;

namespace Course.Basket.API.Feature.Basket.AddBasketItem
{
    public record AddBasketItemCommand(Guid CourseId , string CourseName , decimal CoursePrice , string? ImageUrl):IRequestByServiceResult;
}
