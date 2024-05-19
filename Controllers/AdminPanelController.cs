using Microsoft.AspNetCore.Mvc;
using FenixLauncher.Functions;

namespace FenixLauncher.Controllers
{
    public class AdminPanelController : Controller
    {

        [HttpGet("adminpanel")]
        public IActionResult Index()
        {
            Security security = new Security();
            Configuration configuration = new Configuration();
            string nombre_sesion = configuration.session_name;
            string sesion = HttpContext.Session.GetString(nombre_sesion);
            if (security.ValidateSessionJWT(sesion))
            {
                ViewBag.dir = System.Environment.CurrentDirectory;
                ViewBag.ses = HttpContext.Session.GetString("app_login");
                ViewBag.usuario_logeado = security.loadTokenName(sesion);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
