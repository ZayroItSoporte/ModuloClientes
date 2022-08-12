using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuloClientes.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClienteAlta()
        {
            DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();
            List<Paises> lpai = db.Paises.ToList();
            ViewBag.lpai = lpai;

            List<Estados> lest = db.Estados.ToList();
            ViewBag.lest = lest;
            return View();
        }

        public ActionResult ClienteListado()
        {
            DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();

            List<Clientes> listadoClientes = db.Clientes.ToList();

            ViewBag.listadoClientes = listadoClientes;

            return View();
        }
    }
}