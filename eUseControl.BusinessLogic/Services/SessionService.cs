using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Entities.Response;
using System.Linq;

namespace eUseControl.BusinessLogic.Services
{
    public class SessionService : SessionApi, ISession
    {
        public ServiceResponse ValidateUserCredential(LoginData user)
        {
            return ReturnCredentialStatus(user);
        }

        public ServiceResponse ValidateUserRegister(RegisterData user)
        {
            return ReturnRegisterStatus(user);
        }

        public CookieResponse GenCookie(string nickname)
        {
            return CookieGeneratorAction(nickname);
        }

        public User GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }
        //public static ServiceResponse<SDbModel> GetByToken(string token)
        //{
        //    return Success(DbContext.Sessions.FirstOrDefault(x => x.Token == token));
        //}
    }
}
