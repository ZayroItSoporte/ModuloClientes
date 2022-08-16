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
            List<Paises> lpai = db.Paises.ToList();
            ViewBag.lpai = lpai;

            List<Estados> lest = db.Estados.ToList();
            ViewBag.lest = lest;

            Clientes cliente = db.Clientes.Where(x => x.CLIENTE == id).SingleOrDefault();
            ViewBag.cliente = cliente;
            ViewBag.id = id;

            return View();
        }

        [HttpPost]
        public JsonResult ClienteInserta(Clientes c)
        {
            try
            {
                DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();
                Clientes nc = c;
                //nc.CLIENTE = "0001";
                //db.Clientes.Add(nc);
                db.Ins_ClientesCatalogo(c.CLIENTE, c.NOMBRE, c.DIRECCION, c.ClieNumInt);

                db.SaveChanges();

                return Json(new { error = false, message = "Correcto" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public  ActionResult LlenaEstados(string Paisid)
        {
            DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();
            List<Estados> lest = db.Estados.ToList();
            //List<Estados> lest = db.Estados.Where(f => f.PaisID == Paisid).ToList();
            ViewBag.lest = lest;

            return View();
        }

        [HttpPost]
        public JsonResult ClienteModifica(Clientes c)
        {
            try
            {
                DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();
                Clientes nc = db.Clientes.Where(x => x.CLIENTE == c.CLIENTE).SingleOrDefault();

                db.Entry(nc).CurrentValues.SetValues(c);
                //nc = c;

                db.SaveChanges();

                return Json(new { error = false, message = "Correcto" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult ClienteListado()
        {
            DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();

            List<Clientes> listadoClientes = db.Clientes.ToList();

            ViewBag.listadoClientes = listadoClientes;

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