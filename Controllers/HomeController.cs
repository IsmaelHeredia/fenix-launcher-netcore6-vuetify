using FenixLauncher.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FenixLauncher.Functions;

namespace FenixLauncher.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Security security = new Security();
            Configuration configuration = new Configuration();
            string nombre_sesion = configuration.session_name;
            string sesion = HttpContext.Session.GetString(nombre_sesion);
            if (security.ValidateSessionJWT(sesion))
            {
                return RedirectToAction("Index", "Administracion");
            }
            else
            {
                return View();
            }
        }
    }
}