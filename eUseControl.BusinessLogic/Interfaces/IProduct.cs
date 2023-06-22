using eUseControl.Domain.Entities.Products;
using eUseControl.Domain.Entities.Response;
using System.Collections.Generic;
using static eUseControl.BusinessLogic.Core.ProductApi;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        Product GetProductById(int id);
        Product GetProductByName(string productName);
        List<Product> GetProductList();
        ServiceResponse ValidateEditProduct(Product product);
        ServiceResponse ValidateDeleteProduct(Product product);
        ServiceResponse ValidateCreateProduct(ProductData product);
    }
}
