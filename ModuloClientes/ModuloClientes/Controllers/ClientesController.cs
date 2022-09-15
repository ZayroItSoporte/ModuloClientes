using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

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
            string user = Session["user"].ToString();
            string tipo = Session["tipo"].ToString();

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
            string user = Session["user"].ToString();
            try
            {
                DbIS_CatalogosEntities db = new DbIS_CatalogosEntities();
                Clientes nc = db.Clientes.Where(x => x.CLIENTE == c.CLIENTE).SingleOrDefault();

                #region Crear la estructura del XML para guardarlo en la bitácora
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.DocumentElement;

                XmlElement elm1 = doc.CreateElement(string.Empty, "BACKUP", string.Empty);
                doc.AppendChild(elm1);
                XmlElement elm2 = doc.CreateElement(string.Empty, "Clientes", string.Empty);
                elm1.AppendChild(elm2);

                foreach (var prop in nc.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    string ValProp = Convert.ToString(prop.GetValue(nc, null));
                    string NomProp = prop.Name;
                    XmlElement elm3 = doc.CreateElement(string.Empty, NomProp, string.Empty);
                    XmlText text1 = doc.CreateTextNode(ValProp);
                    elm2.AppendChild(elm3);
                    elm3.AppendChild(text1);
                }

                string estructura = doc.InnerXml.ToString();
                #endregion

                Bitacora btc = new Bitacora();
                btc.BitConsecutivo = 2;
                btc.BitFecha = DateTime.Now;
                btc.BitUsuario = user;
                btc.BitAccion = "U";
                btc.BitAccionBackup = estructura;

                db.Bitacora.Add(btc);

                db.Entry(nc).CurrentValues.SetValues(c);

                db.SaveChanges(); //<<<--- HABILITAR ESTO PARA DESPUES DE PRUEBAS

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