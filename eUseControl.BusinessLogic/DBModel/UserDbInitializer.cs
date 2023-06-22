using eUseControl.Domain.Entities;
using eUseControl.Domain.Enums;
using eUseControl.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eUseControl.BusinessLogic.DBModel
{
    public class UserDbInitializer
    {
        public static void Seed()
        {
            using (UserContext db = new UserContext())
            {
                if (db.Database.Exists())
                {
                    if (!db.Users.Any())
                    {
                        var users = new List<User>
                        {
                            new User()
                            {
                                NickName = "MaxCode",
                                UserName = "Max",
                                UserSurname = "Saveliev",
                                Password = AuthHelper.GeneratePasswordHash("maxim123"),
                                Email = "max@max.com",
                                RegisterDateTime = DateTime.UtcNow,
                                AccessLevel = URole.ADMINISTRATOR
                            },
                            new User()
                            {
                                NickName = "NadejdaCode",
                                UserName = "Nadejda",
                                UserSurname = "Scurtu",
                                Password = AuthHelper.GeneratePasswordHash("nadejda123"),
                                Email = "nadea@nadea.com",
                                RegisterDateTime = DateTime.UtcNow,
                                AccessLevel = URole.ADMINISTRATOR
                            },
                            new User()
                            {
                                NickName = "VladCode",
                                UserName = "Vladislav",
                                UserSurname = "Grama",
                                Password = AuthHelper.GeneratePasswordHash("vladislav123"),
                                Email = "vlad@vlad.com",
                                RegisterDateTime = DateTime.UtcNow,
                                AccessLevel = URole.ADMINISTRATOR
                            },
                            new User()
                            {
                                NickName = "AnaCode",
                                UserName = "Ana",
                                UserSurname = "Bondari",
                                Password = AuthHelper.GeneratePasswordHash("ana12356"),
                                Email = "ana@ana.com",
                                RegisterDateTime = DateTime.UtcNow,
                                AccessLevel = URole.ADMINISTRATOR
                            },
                            new User()
                            {
                                NickName = "TEST USER",
                                UserName = "TEST",
                                UserSurname = "USER",
                                Password = AuthHelper.GeneratePasswordHash("test123456"),
                                Email = "test@test.com",
                                RegisterDateTime = DateTime.UtcNow,
                                AccessLevel = URole.USER
                            }
                        };
                            users.ForEach(x => db.Users.Add(x));
                            db.SaveChanges();
                    }
                }
            }
            //var rand = new Random();
            //for (var i = 0; i < 10; i++) 
            //{
            //    context.Products.Add(new Product
            //    {
            //        ProductName = $"Product #{i}",
            //        BrandName = $"Brand #{i}",
            //        PromotionalPrice = rand.Next(99),
            //        RegularPrice = rand.Next(99),
            //        Category = $"Category {i}",
            //        Size = (ProductSize)(i % Enum.GetValues(typeof(ProductSize)).Length),
            //        Currency = (CurrencyType)(i % Enum.GetValues(typeof(CurrencyType)).Length)
            //    });
            //}
            //
            //base.Seed(context);
        }
    }
}
