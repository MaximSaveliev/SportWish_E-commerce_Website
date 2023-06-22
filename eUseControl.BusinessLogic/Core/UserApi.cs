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
    public class UserApi
    {
        public List<User> AllUsers()
        {
            List<User> users = null;
            using (var db = new UserContext())
            {
                users = db.Users.OrderBy(x => x.Id).ToList();
            }
            return users;
        }

        public User UserById(int id)
        {
            using (var db = new UserContext())
            {
                var currentUser = db.Users.FirstOrDefault(x => x.Id == id);
                if (currentUser == null)
                {
                    return null;
                }
                return currentUser;
            }
        }

        public ServiceResponse ReturnEditUserStatus(User editUser)
        {
            var response = new ServiceResponse();
            using (var db = new UserContext())
            {
                try
                {
                    var existingUser = db.Users.FirstOrDefault(x => x.Id == editUser.Id);
                    if (existingUser == null)
                    {
                        response.StatusMessage = "User not found";
                        response.Status = false;
                        return response;
                    }
                    var dublicateUser = db.Users.FirstOrDefault(x => (x.Email == editUser.Email || x.NickName == editUser.NickName) && x.Id != editUser.Id);
                    if (dublicateUser != null) 
                    {
                        response.StatusMessage = "Username or email already exists for another user";
                        response.Status = false;
                        return response;
                    }
                    else
                    {
                        existingUser.NickName = editUser.NickName;
                        existingUser.UserName = editUser.UserName;
                        existingUser.UserSurname = editUser.UserSurname;
                        existingUser.Email = editUser.Email;
                        existingUser.AccessLevel = editUser.AccessLevel;

                        db.SaveChanges();

                        response.StatusMessage = "User updated successfully";
                        response.Status = true;
                        return response;
                    }
                }
                catch (Exception)
                {
                    response.StatusMessage = "An error occurred while updating the user";
                    response.Status = false;
                }
                return response;
            }
        }

        public ServiceResponse ReturnDeleteUserStatus(User deleteUser)
        {
            var response = new ServiceResponse();
            using (var db = new UserContext())
            {
                try
                {
                    var existingUser = db.Users.FirstOrDefault(x => x.Id == deleteUser.Id);
                    if (existingUser == null)
                    {
                        response.StatusMessage = "User not found";
                        response.Status = false;
                        return response;
                    }

                    using (var dbCart = new CartContext()) 
                    {
                        var items = dbCart.Carts
                            .Where(item => item.CartId == deleteUser.Id)
                            .ToList();
                        foreach (var item in items)
                        {
                            var userCartItem = dbCart.Carts.FirstOrDefault(x => x.CartId == item.CartId);
                            //using (var dbProduct = new ProductContext())
                            //{
                            //    var product = dbProduct.Products.FirstOrDefault(x => x.Id == userCartItem.ProductId);
                            //    product.Amout += userCartItem.Quantity;
                            //    dbProduct.SaveChanges();
                            //}
                            dbCart.Carts.Remove(userCartItem);
                            dbCart.SaveChanges();
                        }
                    }
                    db.Users.Remove(existingUser);
                    db.SaveChanges();

                    response.StatusMessage = "User deleted successfully";
                    response.Status = true;
                }
                catch (Exception)
                {
                    response.StatusMessage = "An error occurred while deleting the user";
                    response.Status = false;
                }
                return response;
            }
        }
        public List<Product> GetCartProducts(int userId)
        {
            using (var dbCart = new CartContext())
            {
                var userCartItems = dbCart.Carts
                    .Where(item => item.CartId == userId)
                    .ToList();

                var productIds = userCartItems.Select(item => item.ProductId).ToList();

                using (var dbProduct = new ProductContext())
                {
                    var products = dbProduct.Products
                        .Where(product => productIds.Contains(product.Id))
                        .ToList();

                    return products;
                }
            }
        }
    }
}
