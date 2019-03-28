using esanz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace esanz.Controllers
{
    public class CategoriaController : Controller
    {
        DAcategoria objDAcategoria = new DAcategoria();
        // GET: Categoria
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar()
        {
            return Json(objDAcategoria.getLstCategoria(), JsonRequestBehavior.AllowGet);
        }
    }
}