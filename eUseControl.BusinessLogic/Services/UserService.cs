using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Entities.Products;
using eUseControl.Domain.Entities.Response;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace eUseControl.BusinessLogic.Services
{
    public class UserService : UserApi, IUser
    {
        public ServiceResponse ValidateEditUser(User data)
        {
            return ReturnEditUserStatus(data);
        }

        public ServiceResponse ValidateDeleteUser(User user)
        {
            return ReturnDeleteUserStatus(user);
        }

        public List<User> GetUserList()
        {
            return AllUsers();
        }

        public User GetUserById(int id)
        {
            return UserById(id);
        }

        public List<Product> GetUserCartProducts(int id)
        {
            return GetCartProducts(id);
        }
    }
}
