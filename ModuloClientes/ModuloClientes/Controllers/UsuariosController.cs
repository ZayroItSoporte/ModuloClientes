using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuloClientes.Controllers
{
    public class UsuariosController : Controller
    {
        public ActionResult UsuarioAlta(string id)
        {
            DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();

            Clientes cliente = db.Clientes.Where(x => x.CLIENTE == id).SingleOrDefault();
            ViewBag.cliente = cliente;
            ViewBag.id = id;

            return View();
        }

        public ActionResult UsuarioListado()
        {
            return View();
        }

        public JsonResult CargarUsuarios()
        {
            try
            {
                DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();
                List<Usuarios> listado = db.Usuarios.ToList();

                var jsonOrdenes = Json(listado, JsonRequestBehavior.AllowGet);
                jsonOrdenes.MaxJsonLength = Int32.MaxValue;

                return jsonOrdenes;

            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UsuarioInserta(Usuarios user)
        {
            try
            {
                DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();

                db.Usuarios.Add(new Usuarios
                {
                    UsuUsuario = user.UsuUsuario,
                    UsuContrasena = user.UsuContrasena,
                    UsuCorreo = user.UsuCorreo,
                    UsuNombre = user.UsuNombre,
                    UsuTipo = user.UsuTipo,
                    UsuActivo = user.UsuActivo,
                });

                return Json(new { error = false, message = "Correcto" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}