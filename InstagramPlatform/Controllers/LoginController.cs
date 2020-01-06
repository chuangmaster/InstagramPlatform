using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstagramPlatform.Controllers
{
    public class LoginController : Controller
    {
        [Route("~/Login")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("~/Auth")]
        public ActionResult Auth(string code)
        {
            return View();
        }
    }
}