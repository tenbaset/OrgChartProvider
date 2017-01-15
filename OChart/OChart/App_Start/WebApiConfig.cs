using OChart.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace OChart {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {
                    id = RouteParameter.Optional
                }
            );
            // Note: Important to use the cache layer generally - often the 'adapter' layer will issue
            // requests to resolve siblings by traversing up to parent and down children.  This could lead
            // to many requests for the same data, which is accelerated by a cache.

            // DummyInfoProvider uses a hard-coded dataset that @dabeing uses (well, part of it).
            // Real uses of this will want to replace with ADInfoProvider or DBInfoProvider or
            // another implementation, but still behind a cache layer.
            OrgChartController.SetInfoProvider(new InfoProvider.InfoProviderCacheLayer(new InfoProvider.DummyInfoProvider()));


        }

    }
}
