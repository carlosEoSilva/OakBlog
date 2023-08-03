using System.Web.Mvc;
using System.Web.Routing;

namespace UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //note-16
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Login",
                url: "Admin/Login",
                defaults: new
                {
                    controller= "Login",
                    action= "Index"
                });

            //rotas para o template Viziew ======================================

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
                );

            routes.MapRoute(
                name: "Category",
                url: "{CategoryName}",
                defaults: new
                {
                    controller = "Home",
                    action = "CategoryPostList",
                    categoryName = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "PostDetail",
                url: "{CategoryName}/{SeoLink}/{ID}",
                defaults: new
                {
                    controller = "Home",
                    action = "PostDetail",
                    ID = UrlParameter.Optional,
                    categoryName = UrlParameter.Optional,
                    seoLink = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Default", 
                url: "", 
                defaults: new 
                { 
                    controller= "Home", 
                    action= "Index" 
                });

            //Rotas do template Mag =========================================
            //routes.MapRoute(
            //    name: "HomeMag",
            //    url: "HomeMag",
            //    defaults: new
            //    {
            //        controller = "HomeMag",
            //        action = "Index"
            //    });

            //routes.MapRoute(
            //    name: "CategoryMag",
            //    url: "{CategoryName}",
            //    defaults: new
            //    {
            //        controller = "HomeMag",
            //        action = "CategoryPostListMag",
            //        categoryName = UrlParameter.Optional
            //    });

            //routes.MapRoute(
            //    name: "PostDetailMag",
            //    url: "{CategoryName}/{SeoLink}/{ID}",
            //    defaults: new
            //    {
            //        controller = "HomeMag",
            //        action = "PostDetailMag",
            //        ID = UrlParameter.Optional,
            //        categoryName = UrlParameter.Optional,
            //        seoLink = UrlParameter.Optional
            //    });
        }
    }
}
