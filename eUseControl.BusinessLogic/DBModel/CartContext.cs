using eUseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DBModel
{
    public class CartContext : DbContext
    {
        public CartContext() : base("name=TESTTEST")
        {
            EnsureDatabaseCreated();
        }
        public virtual DbSet<Cart> Carts { get; set; }
        public void EnsureDatabaseCreated()
        {
            if(!Database.Exists())
            {
                Database.Create();
            }
        }
    }
}
