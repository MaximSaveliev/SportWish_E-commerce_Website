using eUseControl.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DBModel
{
    public class ProductSizeContext : DbContext
    {
        public ProductSizeContext() : base("name=TESTTEST") { }

        public DbSet<ProductSize> ProductSizes { get; set; }
    }
}
