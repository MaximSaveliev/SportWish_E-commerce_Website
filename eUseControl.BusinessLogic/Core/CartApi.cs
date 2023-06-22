using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Entities.Products;
using eUseControl.Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Core
{
    public class CartApi
    {
        public List<Cart> AllCartItems(User user)
        {
            using (var db = new CartContext())
            {
                try
                {
                    var existingCart = db.Carts.FirstOrDefault(x => x.CartId == user.Id);
                    if (existingCart == null)
                    {
                        return new List<Cart>();
                    }
                    else
                    {
                        var items = db.Carts
                            .Where(item => item.CartId == existingCart.CartId)
                            .OrderBy(item => item.ProductId)
                            .ToList();
                        foreach (var item in items)
                        {
                            using (var dbProduct = new ProductContext())
                            {
                                var product = dbProduct.Products.FirstOrDefault(x => x.Id == item.ProductId);
                                item.Product = product;
                            }
                        }

                        return items.Select(item => new Cart
                        {
                            Product = item.Product,
                            Quantity = item.Quantity,
                        }).ToList();
                    }
                }
                catch (Exception)
                {
                    return new List<Cart>();
                }
            }
        }

        public ServiceResponse ReturnAddToCart(Product data, int userId)
        {
            var response = new ServiceResponse();
            using (var db = new CartContext())
            {
                try
                {
                    var existingCart = db.Carts.FirstOrDefault(x => (x.CartId == userId) && (x.ProductId == data.Id));
                    if (existingCart != null)
                    {
                        if (existingCart.ProductId == data.Id)
                        {
                            existingCart.Quantity++;
                            response.StatusMessage = "Item added to cart";
                            response.Status = true;
                        }
                        else
                        {
                            existingCart.ProductId = data.Id;
                            existingCart.Quantity = 1;
                            existingCart.Product = data;

                            response.StatusMessage = "Item added to cart";
                            response.Status = true;
                        }
                    }
                    else if(existingCart == null)
                    {
                        var newCart = new Cart()
                        {
                            CartId = userId,
                            ProductId = data.Id,
                            Quantity = 1
                        };
                        db.Carts.Add(newCart);
                        response.StatusMessage = "Item added to cart";
                        response.Status = true;
                    }
                    else
                    {
                        response.StatusMessage = "An error occurred while adding the item";
                        response.Status = false;
                    }
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    response.StatusMessage = "An error occurred while adding the item";
                    response.Status = false;
                }
                return response;
            }
        }

        public ServiceResponse ReturnDeleteFromCart(Product data, int userId)
        {
            var response = new ServiceResponse();
            using (var db = new CartContext())
            {
                try
                {
                    var existingCart = db.Carts.FirstOrDefault(x => (x.CartId == userId) && (x.ProductId == data.Id));
                    if (existingCart != null)
                    {
                        using (var dbProduct = new ProductContext())
                        {
                            var currentProduct = dbProduct.Products.FirstOrDefault(x => x.Id == data.Id);
                            if (currentProduct == null)
                            {
                                response.StatusMessage = "Product not found";
                                response.Status = false;
                                return response;
                            }
                        }
                        db.Carts.Remove(existingCart);
                        db.SaveChanges();
                        response.StatusMessage = "Product deleted successfully";
                        response.Status = true;
                        return response;
                    }
                    response.StatusMessage = "Product not found";
                    response.Status = false;
                    return response;
                }
                catch (Exception)
                {
                    response.StatusMessage = "An error occurred while adding the item";
                    response.Status = false;
                }
                return response;
            }
        }
    }
}
