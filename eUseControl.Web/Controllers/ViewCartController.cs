using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Web.ActionAtributes;
using eUseControl.Web.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace WebAplication.Controllers
{
    public class ViewCartController : BaseController
    {
        private readonly ICart _cart;
        private readonly IProduct _product;
        private readonly IUser _user;

        public ViewCartController()
        {
            var bl = new BusinessLogic();
            _cart = bl.GetCartBL();
            _product = bl.GetProductBL();
            _user = bl.GetUsertBL();
        }

        // GET: ViewCart
        [AuthorizedMod]
        [HttpGet]
        public ActionResult Index(int userId)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(id => id.Id == userId);
                return View(_cart.GetCartItemList(user));
            }
        }

        [AuthorizedMod]
        [HttpPost]
        public ActionResult DeleteFromCart(int productId, int userId)
        {
            var product = _product.GetProductById(productId);
            if (product != null)
            {
                var response = _cart.ValidateDeleteFromCart(product, userId);
                if (response.Status)
                {
                    return RedirectToAction("Product", "ProductDetail", new { productName = product.ProductName.Replace(" ", "") });
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [AuthorizedMod, HttpPost]
        public ActionResult AddToCart(int productId, int userId)
        {
            var product = _product.GetProductById(productId);
            if (product != null)
            {
                var response = _cart.ValidateAddToCart(product, userId); 
                if (response.Status)
                {
                    using (var db = new ProductContext())
                    {
                        var currentProduct = db.Products.FirstOrDefault(x => x.Id == productId);
                        if (currentProduct == null)
                        {
                            ModelState.AddModelError("", response.StatusMessage);
                        }
                        else
                        {
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Product", "ProductDetail", new { productName = product.ProductName.Replace(" ", "") });
                }
                else
                {
                    // There was an error adding the product
                    ModelState.AddModelError("", response.StatusMessage);
                    return RedirectToAction("Product", "ProductDetail", new { productName = product.ProductName.Replace(" ", "") });
                }
            }
            else
            {
                return RedirectToAction("Product", "ProductDetail", new { productName = product.ProductName.Replace(" ", "") });
            }
        }

        [AuthorizedMod]
        public ActionResult Checkout()
        {
            return View();
        }

        //[RequireUserLogin]
        //[HttpPost]
        //public ActionResult AddProduct(int? product_id)
        //{
        //    if (!product_id.HasValue)
        //        return HttpNoPermission();
        //
        //    var prodResp = ProductService.GetById(product_id.Value);
        //    if (!prodResp.Success)
        //        return HttpNoPermission();
        //
        //    var product = prodResp.Entry;
        //    if (product == null)
        //        return HttpNotFound();
        //
        //    var user = Session.GetUser();
        //    UserService.AddProductToUserCart(user, product);
        //    return Json(new { success = 1 });
        //}
        //
        //[RequireUserLogin]
        //[HttpPost]
        //public ActionResult RemoveProduct(int? product_id)
        //{
        //    if (!product_id.HasValue)
        //        return HttpNoPermission();
        //
        //    var prodResp = ProductService.GetById(product_id.Value);
        //    if (!prodResp.Success)
        //        return HttpNoPermission();
        //
        //    var product = prodResp.Entry;
        //    if (product == null)
        //        return HttpNotFound();
        //
        //    var user = Session.GetUser();
        //    UserService.RemoveProductToUserCart(user, product);
        //    return Json(new { success = 1 });
        //}
    }
}