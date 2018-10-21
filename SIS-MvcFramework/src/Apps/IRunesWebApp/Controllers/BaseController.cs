namespace IRunesWebApp.Controllers
{
    using IRunesWebApp.Data;
    using SIS.MvcFramework;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            this.Db = new IRunesContext();
        }

        protected IRunesContext Db { get; }
    }
}
