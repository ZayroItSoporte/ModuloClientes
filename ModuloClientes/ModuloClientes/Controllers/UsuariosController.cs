using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuloClientes.Controllers
{
    public class UsuariosController : Controller
    {
        public ActionResult UsuarioAlta(int id)
        {
            DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();

            Usuarios usuario = db.Usuarios.Where(x => x.UsuID == id).SingleOrDefault();
            ViewBag.usuario = usuario;
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


        [HttpPost]
        public JsonResult UsuarioModifica(Usuarios user)
        {
            try
            {
                DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();
                Usuarios nc = db.Usuarios.Where(x => x.UsuID == user.UsuID).SingleOrDefault();

                db.Entry(nc).CurrentValues.SetValues(user);

                db.SaveChanges();

                return Json(new { error = false, message = "Correcto" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}