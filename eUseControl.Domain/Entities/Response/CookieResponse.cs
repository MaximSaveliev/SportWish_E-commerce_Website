using System;
using System.Web;

namespace eUseControl.Domain.Entities.Response
{
    public class CookieResponse
    {
        public HttpCookie Cookie { get; set; }
        public DateTime Data { get; set; }
    }
}
