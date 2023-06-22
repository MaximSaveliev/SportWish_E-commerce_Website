using eUseControl.Domain.Entities.Products;
using System.Data.Entity;

namespace eUseControl.BusinessLogic.DBModel
{
    public class SizeContext : DbContext
    {
        public SizeContext() : base("name=TESTTEST"){ }

        public DbSet<Size> Sizes { get; set; }
    }
}
