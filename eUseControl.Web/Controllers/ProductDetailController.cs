using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic.Services;
using eUseControl.Controllers;
using System.Web.Mvc;

namespace WebAplication.Controllers
{
    public class ProductDetailController : eUseBaseController
    {
        private readonly IProduct _product;

        public ProductDetailController()
        {
            var bl = new BusinessLogic();
            _product = bl.GetProductBL();
        }

        // GET: ProductDetail
        public ActionResult Product(string productName)
        {
            var product = _product.GetProductByName(productName);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //public ActionResult Index(int? id)
        //{
        //    if (!id.HasValue)
        //        return HttpNotFound();
        //
        //    var prodresp = ProductService.GetById(id.Value);
        //    if (!prodresp.Success)
        //        return HttpNoPermission();
        //
        //    var product = prodresp.Entry;
        //    if (product == null)
        //        return HttpNotFound();
        //
        //    return View(product);
        //}
    }
}