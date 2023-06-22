using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic.Services;
using eUseControl.Domain.Entities.Products;
using eUseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionService();
        }
        public IProduct GetProductBL()
        {
            return new ProductService();
        }
        public IUser GetUsertBL()
        {
            return new UserService();
        }
        public ICart GetCartBL()
        {
            return new CartService();
        }
    }
}
