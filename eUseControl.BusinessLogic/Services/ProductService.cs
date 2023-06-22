using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Products;
using eUseControl.Domain.Entities.Response;
using System.Collections.Generic;
using System.Linq;

namespace eUseControl.BusinessLogic.Services
{
    public class ProductService : ProductApi, IProduct
    {
        public List<Product> GetProductList()
        {
            return AllProducts();
        }
        public Product GetProductById(int id)
        {
            return ProductById(id);
        }
        public Product GetProductByName(string productName)
        {
            return ProductByName(productName);
        }
        public ServiceResponse ValidateEditProduct(Product product)
        {
            return ReturnEditProductStatus(product);
        }
        public ServiceResponse ValidateDeleteProduct(Product product)
        {
            return ReturnDeleteProductStatus(product);
        }
        public ServiceResponse ValidateCreateProduct(ProductData product)
        {
            return ReturnCreateProductStatus(product);
        }
    }
}
