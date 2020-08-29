using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace Codesanook.OrganizationProfile {
    public class Routes : IRouteProvider {
        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes()) {
                routes.Add(routeDescriptor);
            }
        }

        public IEnumerable<RouteDescriptor> GetRoutes() => new[] {
            new RouteDescriptor {
                Name = "ContactUs",
                Priority = 100,
                Route = new Route(
                    url:"ContactUs",
                    defaults: new RouteValueDictionary {
                        { "controller", "ContactUs" },
                        { "action", "Index" }
                    },
                    constraints: new RouteValueDictionary(),
                    dataTokens: new RouteValueDictionary {
                        { "area", "Codesanook.OrganizationProfile" }
                    },
                    routeHandler: new MvcRouteHandler()
                )
            }
        };
    }
}
