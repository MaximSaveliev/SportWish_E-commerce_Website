using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Products;
using eUseControl.Domain.Entities.Response;
using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Core
{
    public class ProductApi
    {
        public struct ProductData 
        {
            public string ProductName { get; set; }
            public string BrandName { get; set; }
            public string Description { get; set; }
            public float RegularPrice { get; set; }
            public float? PromotionalPrice { get; set; }
            public string Category { get; set; }
            public UGender Gender { get; set; }
            public string Thumbnail { get; set; }
            public ICollection<ProductImg> Images { get; set; }
            public ICollection<ProductSize> Sizes { get; set; }
        }

        public List<Product> AllProducts()
        {
            List<Product> products = null;
            using (var db = new ProductContext())
            {
                products =  db.Products.OrderByDescending(x => x.ProductName).ToList();
            }
            return products;
        }

        public Product ProductById (int id)
        {
            using (var db = new ProductContext())
            {
                var currentProduct = db.Products.FirstOrDefault(x => x.Id == id);
                if (currentProduct == null)
                {
                    return null;
                }
                return currentProduct;
            }
        }

        public Product ProductByName(string productName)
        {
            using (var db = new ProductContext())
            {
                var product = db.Products
                    .Include(x => x.Images)
                    .Include(z => z.Sizes.Select(ps => ps.Size))
                    .FirstOrDefault(p => p.ProductName.Replace(" ", "") == productName);
                return product;
            }
        }

        public ServiceResponse ReturnEditProductStatus(Product data)
        {
            var response = new ServiceResponse();
            using (var db = new ProductContext())
            {
                try
                {
                    var existingProduct = db.Products.FirstOrDefault(x => x.Id == data.Id);
                    if (existingProduct == null)
                    {
                        response.StatusMessage = "Product not found";
                        response.Status = false; 
                        return response;
                    }

                    var duplicateProduct = db.Products.FirstOrDefault(x => (x.ProductName == data.ProductName) && x.Id != data.Id);
                    if (duplicateProduct != null)
                    {
                        response.StatusMessage = "Product with this name already exists";
                        response.Status = false;
                        return response;
                    }
                    else
                    {
                        existingProduct.ProductName = data.ProductName;
                        existingProduct.BrandName = data.BrandName;
                        existingProduct.Description = data.Description;
                        existingProduct.RegularPrice = data.RegularPrice;
                        existingProduct.PromotionalPrice = data.PromotionalPrice;
                        existingProduct.Category = data.Category;
                        existingProduct.Gender = data.Gender;
                        existingProduct.Thumbnail = data.Thumbnail;
                        existingProduct.Images = data.Images;
                        existingProduct.Sizes = data.Sizes;

                        db.SaveChanges();

                        response.StatusMessage = "Product updated successfully";
                        response.Status = true;
                        return response;
                    }
                }
                catch (Exception)
                {
                    response.StatusMessage = "An error occurred while updating the Product";
                    response.Status = false;
                }
                return response;
            }
        }

        public ServiceResponse ReturnCreateProductStatus(ProductData data)
        {
            var response = new ServiceResponse();
            using (var db = new ProductContext())
            {
                try
                {
                    var existingProduct = db.Products.FirstOrDefault(x => x.ProductName == data.ProductName);
                    if (existingProduct != null)
                    {
                        response.StatusMessage = "Product with this name already exists";
                        response.Status = false;
                        return response;
                    }

                    var product = new Product
                    {
                        ProductName = data.ProductName,
                        BrandName = data.BrandName,
                        Description = data.Description,
                        RegularPrice = data.RegularPrice,
                        PromotionalPrice = data.PromotionalPrice,
                        Category = data.Category,
                        Gender = data.Gender,
                        Thumbnail = data.Thumbnail,
                        Images = data.Images,
                        Sizes = data.Sizes,
                    };

                    using (var dbProduct = new ProductContext())
                    {
                        dbProduct.Products.Add(product);
                        dbProduct.SaveChanges();
                    }
                    response.StatusMessage = "Product added successfully";
                    response.Status = true;
                }
                catch (Exception)
                {
                    response.StatusMessage = "An error occurred while adding the product";
                    response.Status = false;
                }
                return response;
            }
        }

        public ServiceResponse ReturnDeleteProductStatus(Product deleteProduct)
        {
            var response = new ServiceResponse();
            using (var dbProduct = new ProductContext())
            {
                try
                {
                    var existingProduct = dbProduct.Products.FirstOrDefault(x => x.Id == deleteProduct.Id);
                    if (existingProduct == null)
                    {
                        response.StatusMessage = "Product not found";
                        response.Status = false;
                        return response;
                    }

                    using (var dbCart = new CartContext())
                    {
                        var items = dbCart.Carts
                            .Where(item => item.ProductId == deleteProduct.Id)
                            .ToList();

                        foreach (var item in items)
                        {
                            var itemCart = dbCart.Carts.FirstOrDefault(x => x.ProductId == item.ProductId);
                            dbCart.Carts.Remove(itemCart);
                        }
                    }

                    dbProduct.Products.Remove(existingProduct);

                    response.StatusMessage = "Product deleted successfully";
                    response.Status = true;

                    dbProduct.SaveChanges();
                }
                catch (Exception)
                {
                    response.StatusMessage = "An error occurred while deleting the Product";
                    response.Status = false;
                }
                return response;
            }
        }
    }
}
