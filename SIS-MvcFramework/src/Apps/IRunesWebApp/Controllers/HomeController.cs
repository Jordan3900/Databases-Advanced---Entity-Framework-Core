namespace IRunesWebApp.Controllers
{
    using SIS.HTTP.Responses;
    using SIS.MvcFramework;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class HomeController : BaseController
    {
        [HttpGet("/")]
        public IHttpResponse Index()
        {
            return this.View("Index");
        }

        [HttpGet("/hello")]
        public IHttpResponse HelloUser()

        {
            return this.View("IndexLoggedIn", new HelloUserViewModel { Username = this.User });
        }
    }

    public class HelloUserViewModel
    {
        public string Username { get; set; }
    }
}
