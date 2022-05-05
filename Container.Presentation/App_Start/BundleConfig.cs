using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace Container.Presentation
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/styles")
                .IncludeDirectory("~/Content/css", "*.css", searchSubdirectories: true));


           bundles.Add(new ScriptBundle("~/bundles/application")
                 .Include("~/scripts/app.js")
                 .Include("~/scripts/app.config.js")
                 .Include("~/scripts/app.run.js")
                 .IncludeDirectory("~/scripts/directives", "*.js", searchSubdirectories: true)
                 .IncludeDirectory("~/source", "*.js", searchSubdirectories: true));

            bundles.Add(new ScriptBundle("~/bundles/components")
                 .Include("~/node_modules/angular/angular.min.js")
                 .Include("~/node_modules/angular-ui-router/release/angular-ui-router.min.js")
                 .Include("~/node_modules/angular-route/angular-route.min.js")
                 .Include("~/node_modules/angular-resource/angular-resource.min.js")
                 .Include("~/node_modules/ng-infinite-scroll/build/ng-infinite-scroll.min.js")
                 .Include("~/node_modules/boostrap/dist/js/bootstrap.min.js")
                 .Include("~/node_modules/@fortawesome/fontawesome-free/js/all.min.js")
                 );

#if DEBUG
            bundles.GetRegisteredBundles().ToList().ForEach(b => b.Transforms.Clear());
#endif

        }

    }
}