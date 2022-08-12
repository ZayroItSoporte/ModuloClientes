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

        public ActionResult ClienteAlta(string id)
        {
            DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();
<<<<<<< Updated upstream
            List<Paises> lpai = db.Paises.ToList();
            ViewBag.lpai = lpai;

            List<Estados> lest = db.Estados.ToList();
            ViewBag.lest = lest;
=======

            Clientes cliente = db.Clientes.Where(x => x.CLIENTE == id).SingleOrDefault();

            ViewBag.cliente = cliente;
>>>>>>> Stashed changes
            return View();
        }
                        
        public ActionResult ClienteListado()
        {            
            return View();
        }

        public JsonResult CargarClientes()
        {
            try
            {
                DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();

                List<Clientes> listadoClientes = db.Clientes.ToList();

                var jsonOrdenes = Json(listadoClientes, JsonRequestBehavior.AllowGet);
                jsonOrdenes.MaxJsonLength = Int32.MaxValue;

                return jsonOrdenes;
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}