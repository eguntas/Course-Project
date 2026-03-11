using Course.Shared;

namespace Course.Basket.API.Feature.Basket.ApplyDiscountCoupon
{
    public record ApplyDiscountCouponCommand(string Coupon,float Rate):IRequestByServiceResult;
   
}
