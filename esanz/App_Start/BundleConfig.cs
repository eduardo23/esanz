using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace esanz.App_Start
{
    /// <summary>
    /// This class handles bundle configuration.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Registers bundles with the application.
        /// </summary>
        /// <param name="bundles">The bundles to register.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/scripts/app/factura.js",
                "~/scripts/app/verFactura.js"
                ));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/*.css"));
        }
    }
}