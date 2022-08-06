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
            return View();
        }

        public ActionResult ClienteListado()
        {
            return View();
        }
    }
}

//< div class= "card card-danger" >

//               < div class= "card-header" >

//                  < h3 class= "card-title" > Different Width </ h3 >

//                    </ div >

//                    < div class= "card-body" >

//                       < div class= "row" >

//                          < div class= "col-3" >

//                             < input type = "text" class= "form-control" placeholder = ".col-3" >

//                               </ div >

//                               < div class= "col-4" >

//                                  < input type = "text" class= "form-control" placeholder = ".col-4" >

//                                    </ div >

//                                    < div class= "col-5" >

//                                       < input type = "text" class= "form-control" placeholder = ".col-5" >

//                                         </ div >

//                                       </ div >

//                                     </ div >

//                                     < !-- /.card - body-- >

//                                   </ div >