using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using API.Controllers;

namespace API.Utils.Helper
{
    /// <summary>
    /// Render razor.
    /// </summary>
    public class RenderRazor
    {
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        protected ControllerContext Context { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:API.Utils.Helper.RenderRazor"/> class.
        /// </summary>
        /// <param name="controllerContext">Controller context.</param>
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

        /// <summary>
        /// Renders the view.
        /// </summary>
        /// <returns>The view.</returns>
        /// <param name="viewPath">View path.</param>
        /// <param name="model">Model.</param>
        public string RenderView(string viewPath, object model)
        {
            return RenderViewToStringInternal(viewPath, model, false).Replace("\n", "");
        }

        /// <summary>
        /// Renders the partial view.
        /// </summary>
        /// <returns>The partial view.</returns>
        /// <param name="viewPath">View path.</param>
        /// <param name="model">Model.</param>
        public string RenderPartialView(string viewPath, object model)
        {
            return RenderViewToStringInternal(viewPath, model, true);
        }

        /// <summary>
        /// Renders the view.
        /// </summary>
        /// <returns>The view.</returns>
        /// <param name="viewPath">View path.</param>
        /// <param name="model">Model.</param>
        /// <param name="controllerContext">Controller context.</param>
        public static string RenderView(string viewPath, object model,
                                        ControllerContext controllerContext)
        {
            RenderRazor renderer = new RenderRazor(controllerContext);
            return renderer.RenderView(viewPath, model);
        }

        /// <summary>
        /// Renders the view.
        /// </summary>
        /// <returns>The view.</returns>
        /// <param name="viewPath">View path.</param>
        /// <param name="model">Model.</param>
        /// <param name="controllerContext">Controller context.</param>
        /// <param name="errorMessage">Error message.</param>
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

        /// <summary>
        /// Renders the partial view.
        /// </summary>
        /// <returns>The partial view.</returns>
        /// <param name="viewPath">View path.</param>
        /// <param name="model">Model.</param>
        /// <param name="controllerContext">Controller context.</param>
        public static string RenderPartialView(string viewPath, object model,
                                                ControllerContext controllerContext)
        {
            RenderRazor renderer = new RenderRazor(controllerContext);
            return renderer.RenderPartialView(viewPath, model);
        }

        /// <summary>
        /// Renders the partial view.
        /// </summary>
        /// <returns>The partial view.</returns>
        /// <param name="viewPath">View path.</param>
        /// <param name="model">Model.</param>
        /// <param name="controllerContext">Controller context.</param>
        /// <param name="errorMessage">Error message.</param>
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

        /// <summary>
        /// Renders the view to string internal.
        /// </summary>
        /// <returns>The view to string internal.</returns>
        /// <param name="viewPath">View path.</param>
        /// <param name="model">Model.</param>
        /// <param name="partial">If set to <c>true</c> partial.</param>
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

        /// <summary>
        /// Creates the controller.
        /// </summary>
        /// <returns>The controller.</returns>
        /// <param name="routeData">Route data.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
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
