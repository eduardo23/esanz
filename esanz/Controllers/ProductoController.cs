using esanz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace esanz.Controllers
{
    public class ProductoController : Controller
    {
        DAproducto objDAproducto = new DAproducto();

        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar(int id)
        {
            return Json(objDAproducto.getLstproducto(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Traer(int id)
        {
            return Json(objDAproducto.getproductobyId(id), JsonRequestBehavior.AllowGet);
        }
        
    }
}