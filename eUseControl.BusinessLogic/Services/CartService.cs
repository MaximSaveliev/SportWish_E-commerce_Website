using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Entities.Products;
using eUseControl.Domain.Entities.Response;
using eUseControl.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace eUseControl.BusinessLogic.Services
{
    public class CartService : CartApi, ICart
    {
        public ServiceResponse ValidateAddToCart(Product item, int userId)
        {
            return ReturnAddToCart(item, userId);
        }

        public ServiceResponse ValidateDeleteFromCart(Product item, int userId)
        {
            return ReturnDeleteFromCart(item, userId);
        }

        public List<Cart> GetCartItemList(User user)
        {
            return AllCartItems(user);
        }
    }
}
