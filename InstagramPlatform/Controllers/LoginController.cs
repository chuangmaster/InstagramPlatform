using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services;

namespace InstagramPlatform.Controllers
{
    public class LoginController : BaseController
    {
        [Route("~/Login")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("~/Auth")]
        public async Task<ActionResult> Auth(string code, string state)
        {
            InstagramService service = new InstagramService();
            var changeResult = await service.ExchangeToken(ClientId, AppSecret, code, "https://29662b76.ngrok.io/Auth");
            return View();
        }
    }
}