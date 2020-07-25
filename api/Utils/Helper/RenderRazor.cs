using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using API.Controllers;

namespace API.Utils.Helper
{
    public class RenderRazor
    {
        protected ControllerContext Context { get; set; }

        public RenderRazor(ControllerContext controllerContext = null)
        {
            if (controllerContext == null)
            {
                if (HttpContext.Current != null)
                    controllerContext = CreateController<ErrorController>().ControllerContext;
                else
                    throw new InvalidOperationException(
                        "RenderRazor must run in the context of an ASP.NET " +
                        "Application and requires HttpContext.Current to be present.");
            }
            Context = controllerContext;
        }

        public string RenderView(string viewPath, object model)
        {
            return RenderViewToStringInternal(viewPath, model, false).Replace("\n", "");
        }

        public string RenderPartialView(string viewPath, object model)
        {
            return RenderViewToStringInternal(viewPath, model, true);
        }

        public static string RenderView(string viewPath, object model,
                                        ControllerContext controllerContext)
        {
            RenderRazor renderer = new RenderRazor(controllerContext);
            return renderer.RenderView(viewPath, model);
        }

        public static string RenderView(string viewPath, object model,
                                        ControllerContext controllerContext,
                                        out string errorMessage)
        {
            errorMessage = null;
            try
            {
                RenderRazor renderer = new RenderRazor(controllerContext);
                return renderer.RenderView(viewPath, model);
            }
            catch (Exception ex)
            {
                errorMessage = ex.GetBaseException().Message;
            }
            return null;
        }

        public static string RenderPartialView(string viewPath, object model,
                                                ControllerContext controllerContext)
        {
            RenderRazor renderer = new RenderRazor(controllerContext);
            return renderer.RenderPartialView(viewPath, model);
        }

        public static string RenderPartialView(string viewPath, object model,
                                                ControllerContext controllerContext,
                                                out string errorMessage)
        {
            errorMessage = null;
            try
            {
                RenderRazor renderer = new RenderRazor(controllerContext);
                return renderer.RenderPartialView(viewPath, model);
            }
            catch (Exception ex)
            {
                errorMessage = ex.GetBaseException().Message;
            }
            return null;
        }

        protected string RenderViewToStringInternal(string viewPath, object model,
                                                    bool partial = false)
        {
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(Context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(Context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("");

            var view = viewEngineResult.View;
            Context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(Context, view,
                                            Context.Controller.ViewData,
                                            Context.Controller.TempData,
                                            sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }



        public static T CreateController<T>(RouteData routeData = null)
                    where T : Controller, new()
        {
            T controller = new T();


            HttpContextBase wrapper = null;
            if (HttpContext.Current != null)
                wrapper = new HttpContextWrapper(HttpContext.Current);

            if (routeData == null)
                routeData = new RouteData();

            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller", controller.GetType().Name.ToLower().Replace("controller", ""));

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
            return controller;
        }
    }
}
