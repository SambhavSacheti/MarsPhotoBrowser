using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Data.Models;
using Client.Services;
using Radzen;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
      
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }); 
            
           builder.Services.AddScoped<MarsPhotoBrowserConfiguration>(sp=> 
                    builder
                    .Configuration
                    .GetSection("RoverConfiguration")
                    .Get<MarsPhotoBrowserConfiguration>());
                    
            builder.Services.AddScoped<PhotoService>();
            builder.Services.AddScoped<ManifestService>();
            await builder.Build().RunAsync();
        }
    }
}
