using eUseControl.Domain.Entities;
using eUseControl.Domain.Enums;
using eUseControl.Helpers;
using System;
using System.Data.Entity;

namespace eUseControl.BusinessLogic.DBModel
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            context.Users.Add(new User
            {
                Name = "Test",
                Surname = "TestTest",
                Email = "test@test.com",
                PasswordHash = AuthHelper.GeneratePasswordHash("testtest"),
                Level = URole.Admin
            });

            context.Users.Add(new User
            {
                Name = "Test 2",
                Surname = "TestTest 2",
                Email = "test2@test.com",
                PasswordHash = AuthHelper.GeneratePasswordHash("testtest")
            });

            var rand = new Random();
            for (var i = 0; i < 10; i++) 
            {
                context.Products.Add(new Product
                {
                    ProductName = $"Product #{i}",
                    BrandName = $"Brand #{i}",
                    PromotionalPrice = rand.Next(99),
                    RegularPrice = rand.Next(99),
                    Category = $"Category {i}",
                    Size = (ProductSize)(i % Enum.GetValues(typeof(ProductSize)).Length),
                    Currency = (CurrencyType)(i % Enum.GetValues(typeof(CurrencyType)).Length)
                });
            }

            base.Seed(context);
        }
    }
}
