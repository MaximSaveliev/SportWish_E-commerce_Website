using eUseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DBModel
{
    public class CartDbInitializer
    {
        public static void Seed()
        {
            using (CartContext db = new CartContext())
            {
                if (db.Database.CreateIfNotExists())
                {
                    if (!db.Carts.Any())
                    {
                        var cart = new List<Cart>
                        {
                            new Cart()
                            {
                                CartId = 999,
                                ProductId = 999
                            }
                        };
                            cart.ForEach(x => db.Carts.Add(x));
                            db.SaveChanges();
                    }
                }
            }
        }
    }
}
