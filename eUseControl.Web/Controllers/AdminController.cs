using eUseControl.BusinessLogic.Services;
using eUseControl.Controllers;
using System.IO;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using eUseControl.Domain.Entities.Products;
using eUseControl.Web.Controllers;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic;
using eUseControl.Web.ActionAtributes;
using eUseControl.Web.Extensions;
using eUseControl.Domain.Enums;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities;
using static eUseControl.BusinessLogic.Core.ProductApi;
using WebAplication.Models;

namespace WebAplication.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IUser _user;
        private readonly ISession _session;
        private readonly IProduct _product;

        public AdminController()
        {
            var bl = new BusinessLogic();
            _user = bl.GetUsertBL();
            _session = bl.GetSessionBL();
            _product = bl.GetProductBL();
        }

        // GET: Admin
        [AdminMod]
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Notifications()
        {
            return View();
        }
        public ActionResult Orders()
        {
            return View();
        }

        [HttpGet]
        [AdminMod]
        public ActionResult AdminProfile()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                User userDB = new User
                {
                    NickName = user.NickName,
                    UserName = user.UserName,
                    UserSurname = user.UserName,
                    Email = user.Email,
                    AccessLevel = user.AccessLevel
                };
                return View(userDB);
            }
        }
        [HttpGet, AdminMod]
        public ActionResult AddProduct()
        {
            ViewBag.Message = "Add Product";
            return View();
        }
        [HttpPost, AdminMod]
        public ActionResult AddProduct(ProductForm product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var productData = new ProductData
            {
                ProductName = product.ProductName,
                BrandName = product.BrandName,
                Description = product.Description,
                RegularPrice = product.RegularPrice,
                PromotionalPrice = product.PromotionalPrice,
                Category = product.Category,
                Gender = product.Gender,
                Thumbnail = product.Thumbnail,
                Images = product.Images,
                Sizes = product.Sizes,
            };
            var response = _product.ValidateCreateProduct(productData);
            if (response.Status)
            {
                return RedirectToAction("ListProducts", "Admin");
            }
            else
            {
                ModelState.AddModelError("Name product already exists", response.StatusMessage);
                return View(product);
            }
        }
        [HttpGet, AdminMod]
        public ActionResult Edit(int id)
        {
            var product = _product.GetProductById(id);
            if (product == null)
            {
                return RedirectToAction("ListProducts", "Admin");
            }
            else
            {
                return View(_product.GetProductById(id));
            }
        }
        [HttpPost, AdminMod, ValidateAntiForgeryToken]
        public ActionResult Edit(Product editproduct)
        {
            var response = _product.ValidateEditProduct(editproduct);
            if (response.Status)
            {
                return RedirectToAction("ListProducts", "Admin");
            }
            else
            {
                ModelState.AddModelError("Product already exists", response.StatusMessage);
                return View(editproduct);
            }
        }
        [AdminMod, HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            var product = _product.GetProductById(id);
            if (product == null)
            {
                return RedirectToAction("ListProducts", "Admin");
            }
            else
            {
                return View(_product.GetProductById(id));
            }
        }
        [AdminMod, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(Product deleteproduct)
        {
            var response = _product.ValidateDeleteProduct(deleteproduct);
            if (response.Status)
            {
                return RedirectToAction("ListProducts", "Admin");
            }
            else
            {
                ModelState.AddModelError("Product already exists", response.StatusMessage);
                return View(deleteproduct);
            }
        }
        [HttpGet]
        [AdminMod]
        public ActionResult ListProducts()
        {
            return View(_product.GetProductList());
        }
        [AdminMod]
        public ActionResult EditUser(int id)
        {
            if (System.Web.HttpContext.Current.GetMySessionObject().AccessLevel == URole.ADMINISTRATOR)
            {
                var user = _user.GetUserById(id);
                if (user == null)
                {
                    return RedirectToAction("ListUsers", "Admin");
                }
                else
                {
                    return View(_user.GetUserById(id));
                }
            }

            else if (System.Web.HttpContext.Current.GetMySessionObject().AccessLevel == URole.USER)
            {
                var db = new UserContext();
                var user = System.Web.HttpContext.Current.GetMySessionObject();
                if (user.Id != id)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(_user.GetUserById(id));
                }
            }

            else
            {
                return HttpNotFound();
            }
        }

        [AdminMod]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(User editUser)
        {
            var sessionObject = System.Web.HttpContext.Current.GetMySessionObject();
            var response = _user.ValidateEditUser(editUser);
            if (response.Status)
            {
                if (sessionObject.Id == editUser.Id)
                {
                    sessionObject.NickName = editUser.NickName;
                    sessionObject.UserName = editUser.UserName;
                    sessionObject.UserSurname = editUser.UserSurname;
                    sessionObject.Email = editUser.Email;
                    sessionObject.AccessLevel = editUser.AccessLevel;

                    var cookieResponse = _session.GenCookie(sessionObject.Email);
                    if (cookieResponse != null)
                    {
                        ControllerContext.HttpContext.Response.Cookies.Add(cookieResponse.Cookie);
                        return RedirectToAction("ListUsers", "Admin");
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    return RedirectToAction("ListUsers", "Admin");
                }
            }
            else
            {
                ModelState.AddModelError("NickName or email already exists", response.StatusMessage);
                return View(editUser);
            }
        }
        [AdminMod]
        public ActionResult DeleteUser(int id)
        {
            var user = _user.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [AdminMod]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(User deleteUser)
        {
            var response = _user.ValidateDeleteUser(deleteUser);
            if (response.Status)
            {
                return RedirectToAction("ListUsers", "Admin");
            }
            else
            {
                ModelState.AddModelError("",response.StatusMessage);
                return View(deleteUser);
            }
        }

        [AdminMod]
        public ActionResult ListUsers()
        {
            return View(_user.GetUserList());
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddProduct(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var resp = ProductService.Add(product);
        //        if (resp.Success)
        //        {
        //            return RedirectToAction("ListProducts");
        //        }
        //
        //        ModelState.AddModelError("", resp.Message);
        //    }
        //
        //    return View(product);
        //}
        //
        //public ActionResult ListProducts()
        //{
        //    var prodResp = ProductService.GetAll();
        //    if (!prodResp.Success)
        //        return HttpNotFound();
        //
        //    return View(prodResp.Entry);
        //}
        //
        //public ActionResult EditProduct(int id)
        //{
        //    var prodResp = ProductService.GetById(id);
        //    if (!prodResp.Success)
        //        return HttpNotFound();
        //
        //    var prod = prodResp.Entry;
        //    return View(prod);
        //}
        //
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditProduct(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var resp = ProductService.Add(product);
        //        if (resp.Success)
        //        {
        //            return RedirectToAction("ListProducts");
        //        }
        //
        //        ModelState.AddModelError("", resp.Message);
        //    }
        //
        //    return View(product);
        //}
        //
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditProductUploadImage(EditProductUploadForm form)
        //{
        //    var prodResp = ProductService.GetById(form.ProductId);
        //    if (!prodResp.Success)
        //        return HttpNoPermission();
        //
        //    var product = prodResp.Entry;
        //    if (product == null)
        //        return HttpNotFound();
        //
        //    for (var i = 0; i < 3; i++)
        //    {
        //        HttpPostedFileBase file = null;
        //
        //        switch(i)
        //        {
        //            case 0:
        //                file = form.Image0; break;
        //            case 1:
        //                file = form.Image1; break;
        //            case 2:
        //                file = form.Image2; break;
        //        }
        //
        //        if (file == null)
        //            continue;
        //
        //        byte[] bytes = new byte[file.ContentLength];
        //        using (BinaryReader theReader = new BinaryReader(file.InputStream))
        //        {
        //            bytes = theReader.ReadBytes(file.ContentLength);
        //        }
        //        string base64 = Convert.ToBase64String(bytes);
        //        var image = $"data:{file.ContentType};base64,{base64}";
        //
        //        switch (i)
        //        {
        //            case 0: product.Image0 = image; break;
        //            case 1: product.Image1 = image; break;
        //            case 2: product.Image2 = image; break;
        //        }
        //    }
        //
        //    ProductService.Edit(product);
        //    return RedirectToAction("EditProduct", new {id = product.Id});
        //}
    }
}