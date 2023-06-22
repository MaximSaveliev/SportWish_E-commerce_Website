using eUseControl.Domain.Entities;
using eUseControl.Domain.Entities.Products;
using eUseControl.Domain.Entities.Response;
using System.Collections.Generic;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface IUser
    {
        User GetUserById(int id);
        List<User> GetUserList();
        List<Product> GetUserCartProducts(int id);
        ServiceResponse ValidateEditUser(User user);
        ServiceResponse ValidateDeleteUser(User user);
    }
}
