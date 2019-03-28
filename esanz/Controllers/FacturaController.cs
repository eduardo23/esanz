using esanz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace esanz.Controllers
{
    public class FacturaController : Controller
    {
        DAfactura objDAfactura= new DAfactura();
        DAcliente objDAcliente = new DAcliente();
        DAmodo_pago objDAmodo_pago = new DAmodo_pago();

        // GET: Factura
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult verFactura(int id)
        {
            ViewBag.ddlCliente = new SelectList(objDAcliente.getLstCliente(), "id_cliente","Nombres");
            ViewBag.ddlForma_pago = new SelectList(objDAmodo_pago.getLstModo_Pago(),"num_pago","nombre");
            return View(objDAfactura.getFacturabyId(id));
        }

        public JsonResult Listar(int num_pago, DateTime fecini, DateTime fecfin, string cliente)
        {
            return Json(objDAfactura.getLstFacturaCab(num_pago, fecini, fecfin, cliente), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Grabar(BEFactura Factura)
        {
            return Json(objDAfactura.setGrabarFactura(Factura), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(int num_factura)
        {
            return Json(objDAfactura.setEliminaFactura(num_factura), JsonRequestBehavior.AllowGet);
        }

    }
}