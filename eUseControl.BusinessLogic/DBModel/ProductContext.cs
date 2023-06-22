using eUseControl.Domain.Entities.Products;
using System.Data.Entity;

namespace eUseControl.BusinessLogic.DBModel
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("name=TESTTEST") { }

        public virtual DbSet<Product> Products { get; set; }
    }
}
