using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazr.SiteBuilder
{
    public class SiteBuilderFactory
    {
        private RouteFactory _routeFactory;
        private HtmlRenderer _htmlRenderer;
            
        public SiteBuilderFactory(HtmlRenderer htmlRenderer, RouteFactory routeFactory) 
        {
            _htmlRenderer = htmlRenderer;
            _routeFactory = routeFactory;
        }

        public async Task BuildSiteAsync()
        {
            _routeFactory.GetRoutes();
            await this.BuildRoutesAsync();
 
        }

        public async Task BuildRoutesAsync()
        {
            foreach(var route in _routeFactory.RouteList)
            {
                var html = await _htmlRenderer.Dispatcher.InvokeAsync(async () =>
                {
                    HtmlRootComponent output = await _htmlRenderer.RenderComponentAsync(route.Component);
                    return output.ToHtmlString();
                });

                string htmlFlePath = $"{Environment.CurrentDirectory}/wwwroot{route.Route}.html";

                // Create path if it dosn't exist               
                var folder = Path.GetDirectoryName(htmlFlePath);
                if (!Directory.Exists(folder))
                {
                    DirectoryInfo di = Directory.CreateDirectory(folder);
                }

                // Writr filw
                await File.WriteAllTextAsync(htmlFlePath, html);
            }
        }

        public async Task<string> BuildPageAsync(Type component)
        {
            HtmlRootComponent output = await _htmlRenderer.RenderComponentAsync(component);
            return output.ToHtmlString();
        }
    }
}
