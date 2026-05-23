using Course.Web.PageModel;
using Course.Web.Pages.Order.ViewModel;
using Course.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Course.Web.Pages.Order
{
    [Authorize]
    public class HistoryModel(OrderService orderService) : BasePageModel
    {
        public List<OrderHistoryViewModel> OrderHistoryList { get; set; } = null!;

        public async Task<IActionResult> OnGet()
        {
            var response = await orderService.GetHistory();


            if (response.IsFailure) return ErrorPage(response);

            OrderHistoryList = response.Data!;


            return Page();
        }
    }
}
