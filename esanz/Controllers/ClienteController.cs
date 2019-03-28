using esanz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace esanz.Controllers
{
    public class ClienteController : Controller
    {
        DAcliente objDAcliente = new DAcliente();

        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar()
        {
            return Json(objDAcliente.getLstCliente(), JsonRequestBehavior.AllowGet);
        }

    }
}