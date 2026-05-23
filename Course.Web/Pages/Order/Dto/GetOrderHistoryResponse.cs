using Course.Web.Pages.Order.ViewModel;

namespace Course.Web.Pages.Order.Dto
{
    public record GetOrderHistoryResponse(DateTime Created, decimal TotalPrice, List<OrderItemViewModel> Items);
}
