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
    }
}