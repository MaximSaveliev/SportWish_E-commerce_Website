using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Entities.Response;
using eUseControl.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eUseControl.BusinessLogic.Core
{
    public class SessionApi
    {
        public struct LoginData
        {
            public string NickName { get; set; }
            public string Password { get; set; }
            public DateTime Time { get; set; }
        }

        public ServiceResponse ReturnCredentialStatus(LoginData user)
        {
            var hPass = AuthHelper.GeneratePasswordHash(user.Password);
            using (var db = new UserContext())
            {
                var userData = db.Users.FirstOrDefault(x => x.NickName == user.NickName && x.Password == hPass);
                if (userData == null)
                {
                    return new ServiceResponse { Status = false, StatusMessage = "Email or Password is Incorrect" };
                }
            }
            return new ServiceResponse { Status = true, StatusMessage = string.Empty };
        }

        public struct RegisterData
        {
            public string NickName { get; set; }
            public string UserName { get; set; }
            public string UserSurname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public DateTime Time { get; set; }
        }

        public ServiceResponse ReturnRegisterStatus(RegisterData newUser)
        {
            var response = new ServiceResponse();
            using (var db = new UserContext())
            {
                try
                {
                    var existingUser = db.Users.FirstOrDefault(x => x.Email == newUser.Email || x.NickName == newUser.NickName);
                    if (existingUser != null)
                    {
                        response.StatusMessage = "User with this email or nickname already exists";
                        response.Status = false;
                        return response;
                    }

                    var user = new User
                    {
                        NickName = newUser.NickName,
                        UserName = newUser.UserName,
                        UserSurname = newUser.UserSurname,
                        Email = newUser.Email,
                        Password = AuthHelper.GeneratePasswordHash(newUser.Password),
                        RegisterDateTime = DateTime.Now,
                        LoginDateTime = DateTime.Now,
                        AccessLevel = Domain.Enums.URole.USER
                    };
                    using (var dbUser = new UserContext())
                    {
                        dbUser.Users.Add(user);
                        dbUser.SaveChanges();
                    }

                    response.StatusMessage = "User registered successfully";
                    response.Status = true;
                }
                catch (Exception)
                {
                    response.StatusMessage = "An error occurred while registering the user";
                    response.Status = false;
                }
                return response;
            }
        }
        public CookieResponse CookieGeneratorAction(string nickname)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(nickname)
            };

            using (var db = new SessionContext())
            {
                SDbModel current;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(nickname))
                {
                    current = (from x in db.Sessions where x.NickName == nickname select x).FirstOrDefault();
                }
                else
                {
                    current = (from x in db.Sessions where x.NickName == nickname select x).FirstOrDefault();
                }

                if (current != null)
                {
                    current.CookieString = apiCookie.Value;
                    current.ExpireTime = DateTime.Now.AddMinutes(120);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(current).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new SDbModel
                    {
                        NickName = nickname,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(120)
                        // Может стоит добавть еще
                    });
                    db.SaveChanges();
                }
            }
            return new CookieResponse
            {
                    Cookie = apiCookie,
                    Data = DateTime.Now
            };
        }
        public User UserCookie(string cookie)
        {
            SDbModel session;
            User currentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(x => x.CookieString == cookie && x.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.NickName))
                {
                    currentUser = db.Users.FirstOrDefault(x => x.NickName == session.NickName);
                }
                else
                {
                    currentUser = db.Users.FirstOrDefault(x => x.NickName == session.NickName);
                }
            }
            if (currentUser == null) return null;

            var user = new User
            {
                Id = currentUser.Id,
                NickName = currentUser.NickName,
                UserName = currentUser.UserName,
                UserSurname = currentUser.UserSurname,
                Email = currentUser.Email,
                AccessLevel = currentUser.AccessLevel,
                // Добавил поля
            };
            return user;
        }
    }
}
