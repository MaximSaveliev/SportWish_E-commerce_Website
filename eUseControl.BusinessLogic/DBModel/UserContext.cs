using eUseControl.Domain.Entities;
using System.Data.Entity;

namespace eUseControl.BusinessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext() 
            : base("name=TEST_WEB_DB") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
