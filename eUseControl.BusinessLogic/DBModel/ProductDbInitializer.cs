using eUseControl.Domain.Entities.Products;
using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DBModel
{
    public class ProductDbInitializer
    {
        public static void Seed()
        {
            using (ProductContext db = new ProductContext())
            {
                if (db.Database.Exists())
                {
                    if (!db.Products.Any())
                    {
                        var products = new List<Product>
                        {
                            new Product()
                            {
                                ProductName = "Air Jordan XXXVII Low GC",
                                BrandName = "Nike",
                                Description = "Something about Air Jordan XXXVII Low GC",
                                RegularPrice = 195,
                                Category = "Sneakers",
                                Gender = UGender.MAN,
                                Thumbnail = "https://static.nike.com/a/images/c_limit,w_592,f_auto/t_product_v1/14b9f008-ae3e-4baa-b504-30c0dfb9de40/air-jordan-xxxvii-low-gc-mens-basketball-shoes-T0tlwS.png",
                                Images = new List<ProductImg>()
                                {
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5,q_80/d46e0b95-e7c9-4b7b-9320-3bb098ec677a/air-jordan-xxxvii-low-gc-mens-basketball-shoes-T0tlwS.png" },
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5/3f7a370b-f1a3-40f5-af4b-08c3db27a6eb/air-jordan-xxxvii-low-gc-mens-basketball-shoes-T0tlwS.png" },
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5,q_80/7cca9dc3-e846-48de-99f2-d42a844fbceb/air-jordan-xxxvii-low-gc-mens-basketball-shoes-T0tlwS.png" },
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5,q_80/dc779e8f-eea4-41b8-8fb2-ca466af161a0/air-jordan-xxxvii-low-gc-mens-basketball-shoes-T0tlwS.png" },
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5,q_80/32fdfc53-27bd-43bb-b774-8d3d84e7e794/air-jordan-xxxvii-low-gc-mens-basketball-shoes-T0tlwS.png" },
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5,q_80/e0945f88-38f6-4a1e-b8f8-cda518db2e52/air-jordan-xxxvii-low-gc-mens-basketball-shoes-T0tlwS.png" }
                                },
                                Sizes = new List<ProductSize>
                                {
                                    new ProductSize { SizeId = 4 },
                                    new ProductSize { SizeId = 5 },
                                    new ProductSize { SizeId = 6 },
                                    new ProductSize { SizeId = 7 },
                                    new ProductSize { SizeId = 8 },
                                    new ProductSize { SizeId = 9 },
                                    new ProductSize { SizeId = 10 },
                                    new ProductSize { SizeId = 11 }
                                }
                            },
                            new Product()
                            {
                                ProductName = "Nike Waffle Debut",
                                BrandName = "Nike",
                                Description = "Something about Nike Waffle Debut",
                                RegularPrice = 80,
                                Category = "Sneakers",
                                Gender = UGender.WOMAN,
                                Thumbnail = "https://static.nike.com/a/images/c_limit,w_592,f_auto/t_product_v1/91d6ba5c-bfbb-4fdd-81d2-02c705a75cae/waffle-debut-womens-shoes-5Rl1KL.png",
                                Images = new List<ProductImg>()
                                {
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5/078ee208-496e-4fca-a0e3-aa5ee4fd856a/waffle-debut-womens-shoes-5Rl1KL.png" },
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5/787c1e2a-11d4-4a76-840b-41a4c7f56878/waffle-debut-womens-shoes-5Rl1KL.png" },
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5/2bd82d04-6380-4e9f-afbb-bd8777c24d8e/waffle-debut-womens-shoes-5Rl1KL.png" },
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5,q_80/e0c23371-b11d-4ff5-8dde-0a2868a3614e/waffle-debut-womens-shoes-5Rl1KL.png" },
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5,q_80/e3cc9082-1448-487d-9e6b-c0ee33cb14ab/waffle-debut-womens-shoes-5Rl1KL.png" }
                                },
                                Sizes = new List<ProductSize>
                                {
                                    new ProductSize { SizeId = 3 },
                                    new ProductSize { SizeId = 4 },
                                    new ProductSize { SizeId = 5 },
                                    new ProductSize { SizeId = 6 },
                                    new ProductSize { SizeId = 7 },
                                }
                            },
                            new Product()
                            {
                                ProductName = "Nike Dri-FIT Challenger",
                                BrandName = "Nike",
                                Description = "Something about Nike Dri-FIT Challenger",
                                RegularPrice = 75,
                                Category = "Pants",
                                Gender = UGender.MAN,
                                Thumbnail = "https://static.nike.com/a/images/c_limit,w_592,f_auto/t_product_v1/5c7dcb9e-cb68-456e-94ec-f9f7db26440b/dri-fit-challenger-mens-woven-running-pants-VwT2cP.png",
                                Images = new List<ProductImg>()
                                {
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5/792ab50c-531c-415f-8eae-f5ad8ea2c295/dri-fit-challenger-mens-woven-running-pants-VwT2cP.png" },
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5/ca29289e-31b2-4c3a-8dc9-e7b11500be89/dri-fit-challenger-mens-woven-running-pants-VwT2cP.png" },
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5/cc14bf8f-e1cd-4a29-878d-3d799f8ef83b/dri-fit-challenger-mens-woven-running-pants-VwT2cP.png" },
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5/7f9741ad-aff5-4617-bcf2-81be43068e74/dri-fit-challenger-mens-woven-running-pants-VwT2cP.png" },
                                    new ProductImg() { Image = "https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5/36ffbec0-2ac8-4a57-99d9-4ed3abc04bee/dri-fit-challenger-mens-woven-running-pants-VwT2cP.png" }
                                },
                                Sizes = new List<ProductSize>
                                {
                                    new ProductSize { SizeId = 18 },
                                    new ProductSize { SizeId = 19 },
                                    new ProductSize { SizeId = 20 },
                                    new ProductSize { SizeId = 21 }
                                }
                            }
                        };
                            products.ForEach(x => db.Products.Add(x));
                            db.SaveChanges();
                    }
                }
            }
        }
    }
}
