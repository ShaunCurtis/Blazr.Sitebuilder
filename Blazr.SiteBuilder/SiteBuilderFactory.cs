using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazr.SiteBuilder
{
    public class SiteBuilderFactory
    {
        private IServiceProvider _serviceProvider;
        private RouteFactory _routeFactory;
            
        public SiteBuilderFactory(IServiceProvider serviceProvider, RouteFactory routeFactory) 
        {
            _serviceProvider = serviceProvider;
            _routeFactory = routeFactory;
        }

        public async Task BuildSiteAsync()
        {
            await this.BuildRoutesAsync();
        }

        public async Task BuildRoutesAsync()
        {
            foreach(var route in _routeFactory.RouteList)
            {
                using var htmlRenderer = ActivatorUtilities.CreateInstance<HtmlRenderer>(_serviceProvider);

                var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
                {
                    HtmlRootComponent output = await htmlRenderer.RenderComponentAsync(route.Component);
                    return output.ToHtmlString();
                });

                string htmlFlePath = $"{Environment.CurrentDirectory}/wwwroot{route.Route}.html";

                // Create path if it doesn't exist               
                var folder = Path.GetDirectoryName(htmlFlePath);

                if (folder is not null && !Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                // Write file
                await File.WriteAllTextAsync(htmlFlePath, html);
            }
        }
    }
}
