using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Coleta
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
