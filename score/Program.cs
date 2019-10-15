using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using score.Configuration.Logging;
using Serilog;

namespace score
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = LoggingConfig.ConfigureLogger("ScoreApi");
            Log.Information("Application Started");
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)           
            .UseStartup<Startup>();
    }
}
