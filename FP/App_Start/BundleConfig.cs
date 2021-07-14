using System.Web.Optimization;

namespace FP
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                       "~/Scripts/angular.js",
                       "~/Scripts/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/multiselect").Include(
                     "~/Scripts/angularjs-dropdown-multiselect.min.js"
                    ));

          

                 //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                 //  "~/Scripts/jquery-3.0.0.min.js",
                 //  "~/Scripts/jquery-ui.js",
                 //  "~/Scripts/jquery-ui.css"
                //  ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/navcss").Include(
                      "~/Content/nav.css"));



            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                "~/Scripts/toastr.min.css",
                "~/Scripts/toastr.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/safpApp").Include(
                "~/Scripts/app/fpApp.js",
                "~/Scripts/app/services/fpService.js"));


            bundles.Add(new ScriptBundle("~/bundles/Home").Include(
                "~/js/jquery-3.3.1.js",
                "~/js/popper.min.js",
                "~/js/bootstrap.js",
                "~/js/easing.js",
                "~/js/lightbox.min.js",
                "~/js/owl.carousel.min.js"
                ));

            bundles.Add(new StyleBundle("~/Content/homecss").Include(
                "~/Content/bootstrap.css",
                      "~/css/lightbox.css",
                      "~/css/animate.css",
                      "~/css/owl.carousel.min.css",
                      "~/css/owl.theme.default.css",
                      "~/css/style.css",
                      "~/css/font-awesome.min.css"
                      ));



        }
    }
}
