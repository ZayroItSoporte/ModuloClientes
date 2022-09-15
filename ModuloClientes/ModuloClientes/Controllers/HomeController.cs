using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuloClientes.Controllers
{
    public class HomeController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        public JsonResult Ingresar(string user, string pass)
        {
            try
            {
                DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();

                var usuario = db.Usuarios.Where(x => x.UsuUsuario == user && x.UsuContrasena == pass).SingleOrDefault();

                if (usuario == null)
                {
                    return Json(new { error = false, message = "Incorrecto" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Session["user"] = user;
                    Session["tipo"] = usuario.UsuTipo;
                    return Json(new { error = false, message = "Correcto" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}