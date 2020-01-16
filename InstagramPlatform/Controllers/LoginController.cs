using System.Threading.Tasks;
using System.Web.Mvc;
using Services;


namespace InstagramPlatform.Controllers
{
    public class LoginController : BaseController
    {
        [Route("~/Login")]
        public ActionResult Login()
        {
            if (Session["UserIdentify"] != null)
            {
                TempData["Message"] = "You have already logged in";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [Route("~/Logout")]
        public ActionResult Logout()
        {
            if (Session["UserIdentify"] != null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login");
        }

        [Route("~/Auth")]
        public async Task<ActionResult> Auth(string code, string state)
        {
            
            InstagramBasicDisplayService service = new InstagramBasicDisplayService();
            var changeResult = await service.ExchangeToken(ClientId, AppSecret, code, "https://12bdfb65.ngrok.io/Auth");
            if (changeResult != null)
            {
                var profile = await service.GetUserProfile(changeResult.Token);
                Session.Add("UserIdentify", changeResult.UserId);
                Session.Add("UserName", profile.UserName);
            }
            else
            {
                TempData["Message"] = "Login fail";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}