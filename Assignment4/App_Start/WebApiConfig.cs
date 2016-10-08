using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Assignment4
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "IndividualPlayer",
                routeTemplate: "api/{controller}/{Registration_id}",
                defaults: new { Registration_id = RouteParameter.Optional});

            config.Routes.MapHttpRoute(
                name: "AllPlayersNameSearch",
                routeTemplate: "api/{controller}/{field}/{search}",
                defaults: new
                {
                    field = RouteParameter.Optional,
                    search = RouteParameter.Optional

                });

            config.Routes.MapHttpRoute(
                name: "AllPlayers",
                routeTemplate: "api/{controller}",
                defaults: new {id = RouteParameter.Optional}

            );
        }
    }
}
