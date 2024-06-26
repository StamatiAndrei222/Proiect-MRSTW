using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


    public class BundleConfig
    {
    public static void RegisterBundles(BundleCollection bundles)
    {


        // CSS Bundle
        bundles.Add(new StyleBundle("~/bundles/css").Include(
                   "~/Templates/Admin/assets/plugins/pace/pace-theme-flash.css",
                   "~/Templates/Admin/assets/plugins/bootstrap/css/bootstrap.min.css",
                   "~/Templates/Admin/assets/fonts/font-awesome/css/font-awesome.css",
                   "~/Templates/Admin/assets/css/animate.min.css",
                   "~/Templates/Admin/assets/plugins/perfect-scrollbar/perfect-scrollbar.css",
                   "~/Templates/Admin/assets/plugins/morris-chart/css/morris.css",
                   "~/Templates/Admin/assets/plugins/jquery-ui/smoothness/jquery-ui.min.css",
                   "~/Templates/Admin/assets/plugins/rickshaw-chart/css/graph.css",
                   "~/Templates/Admin/assets/plugins/rickshaw-chart/css/detail.css",
                   "~/Templates/Admin/assets/plugins/rickshaw-chart/css/legend.css",
                   "~/Templates/Admin/assets/plugins/rickshaw-chart/css/extensions.css",
                   "~/Templates/Admin/assets/plugins/rickshaw-chart/css/rickshaw.min.css",
                   "~/Templates/Admin/assets/plugins/rickshaw-chart/css/lines.css",
                   "~/Templates/Admin/assets/plugins/jvectormap/jquery-jvectormap-2.0.1.css",
                   "~/Templates/Admin/assets/plugins/icheck/skins/minimal/white.css",
                   "~/Templates/Admin/assets/css/style.css",
                   "~/Templates/Admin/assets/css/responsive.css"
         ));

        // JS Bundle
        bundles.Add(new ScriptBundle("~/bundles/js").Include(
                    "~/Templates/Admin/assets/js/jquery-3.2.1.min.js",
                    "~/Templates/Admin/assets/js/popper.min.js",
                    "~/Templates/Admin/assets/js/jquery.easing.min.js",
                    "~/Templates/Admin/assets/plugins/bootstrap/js/bootstrap.min.js",
                    "~/Templates/Admin/assets/plugins/pace/pace.min.js",
                    "~/Templates/Admin/assets/plugins/perfect-scrollbar/perfect-scrollbar.min.js",
                    "~/Templates/Admin/assets/plugins/viewport/viewportchecker.js",
                    "~/Templates/Admin/assets/plugins/rickshaw-chart/vendor/d3.v3.js",
                    "~/Templates/Admin/assets/plugins/jquery-ui/smoothness/jquery-ui.min.js",
                    "~/Templates/Admin/assets/plugins/rickshaw-chart/js/Rickshaw.All.js",
                    "~/Templates/Admin/assets/plugins/sparkline-chart/jquery.sparkline.min.js",
                    "~/Templates/Admin/assets/js/chart-sparkline.js",
                    "~/Templates/Admin/assets/plugins/easypiechart/jquery.easypiechart.min.js",
                    "~/Templates/Admin/assets/plugins/morris-chart/js/raphael-min.js",
                    "~/Templates/Admin/assets/plugins/morris-chart/js/morris.min.js",
                    "~/Templates/Admin/assets/plugins/jvectormap/jquery-jvectormap-2.0.1.min.js",
                    "~/Templates/Admin/assets/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                    "~/Templates/Admin/assets/plugins/gauge/gauge.min.js",
                    "~/Templates/Admin/assets/plugins/icheck/icheck.min.js",
                    "~/Templates/Admin/assets/js/uni-dashboard.js",
                    "~/Templates/Admin/assets/js/scripts.js"));

            //---------------------Admin Bundles---------------------


        bundles.Add(new StyleBundle("~/bundles/owlcarousel").Include(
                  "~/Templates/Home/lib/owlcarousel/assets/owl.carousel.min.css"));
        bundles.Add(new StyleBundle("~/bundles/customstyle").Include(
                  "~/Templates/Home/css/style.css"));


        bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-{version}.js"));


        bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                  "~/Scripts/bootstrap.bundle.min.js"));

 
        bundles.Add(new ScriptBundle("~/bundles/libraries").Include(
                  "~/Templates/Home/lib/easing/easing.min.js",
                  "~/Templates/Home/lib/waypoints/waypoints.min.js",
                  "~/Templates/Home/lib/counterup/counterup.min.js",
                  "~/Templates/Home/lib/owlcarousel/owl.carousel.min.js"));


        bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                  "~/Templates/Home/js/main.js"));

        BundleTable.EnableOptimizations = true;
    }
}
