using eUseControl.Domain.Entities;
using eUseControl.Domain.Entities.Products;
using eUseControl.Domain.Entities.Response;
using System.Collections.Generic;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface ICart
    {
        ServiceResponse ValidateAddToCart(Product item, int userId);
        ServiceResponse ValidateDeleteFromCart(Product item, int userId);
        List<Cart> GetCartItemList(User user);
    }
}
