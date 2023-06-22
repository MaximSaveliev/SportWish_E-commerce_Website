using eUseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Extensions
{
    public static class HttpContextExtensions
    {
        public static User GetMySessionObject(this HttpContext context)
        {
            return (User)context?.Session["__SessionObject"];
        }

        public static void SetMySessionObject(this HttpContext context, User profile )
        {
            context.Session.Add("__SessionObject", profile);
        }
    }
}