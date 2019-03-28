using esanz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace esanz.Controllers
{
    public class Modo_PagoController : Controller
    {
        DAmodo_pago objDAmodo_pago = new DAmodo_pago();
        // GET: Modo_Pago
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar() {
            return Json(objDAmodo_pago.getLstModo_Pago(), JsonRequestBehavior.AllowGet);
        }
    }
}