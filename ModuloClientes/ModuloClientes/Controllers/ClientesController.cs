using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModuloClientes.Controllers
{
    public class ClientesController : Controller
    {

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

        public ActionResult ClienteListado()
        {
            //DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();

            //var listadoClientes = db.Clientes.Join(db.Estados,
            //    cliente => cliente.EstID,
            //    estado => estado.EstID,
            //    (cliente, estado) => new { cliente, estado })
            //    .Join(db.Paises,
            //    clientes => clientes.cliente.CLIENTE,
            //    paises => paises.PaisID,
            //    (clientes, paises) => new { clientes, paises })
            //    .Select(x => new
            //    {
            //        //s
            //    });

            //ViewBag.listadoClientes = listadoClientes;

            return View();
        }

        [HttpPost]
        public JsonResult ClienteInserta(Clientes c)
        {
            try
            {
                DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();
                Clientes nc = c;
                string clid;
                using (db)
                {
                    int cons = db.Database.SqlQuery<int>("SELECT MAX(CONVERT(int, CLIENTE))+1 AS CLIENTE FROM CLIENTES").FirstOrDefault();
                    clid = cons.ToString();
                    while (clid.Length < 4)
                    {
                        clid = "0" + clid;
                    }
                }

                c.CLIENTE = clid;
                //db.Clientes.Add(nc);
                db.Ins_ClientesCatalogo(c.CLIENTE,
                    c.NOMBRE,
                    c.DIRECCION,
                    c.ClieNumInt,
                    c.ClieNumExt,
                    c.CIUDAD,
                    c.EstID,
                    c.PaisID,
                    c.CliCodPost,
                    c.CliRFC,
                    c.CliIRS,
                    c.TELEFONO,
                    c.Telefono2,
                    c.CliColonia,
                    c.CURP,
                    c.AltaPor,
                    c.URL,
                    c.AgrupCliCtaMex,
                    c.AgrupCliCtaAm,
                    "identificador",
                    c.FAX,
                    "usuario");

                db.SaveChanges();

                return Json(new { error = false, message = "Correcto" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

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

        [HttpPost]
        public JsonResult LlenaEstados(string Paisid)
        {
            try
            {
                DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();
                //List<Estados> lest = db.Estados.ToList();
                List<Estados> lest = db.Estados.Where(f => f.EstPais == Paisid).ToList();
                ViewBag.lest = lest;

                var jsonEst = Json(lest, JsonRequestBehavior.AllowGet);
                jsonEst.MaxJsonLength = Int32.MaxValue;

                return jsonEst;
            }
            catch (Exception x)
            {
                return Json(new { error = false, message = x.Message }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}