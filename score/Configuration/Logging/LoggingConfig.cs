using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using System;
using System.IO;

namespace score.Configuration.Logging
{
    /// <summary>
    /// Configure Logging
    /// </summary>
    /// 
    public class LoggingConfig
    {
        private readonly IConfiguration configuration;

        public LoggingConfig(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Configures serilog
        /// </summary>
        /// <param name="applicationName">Name of the application that generates exception</param>
        /// <returns>Serilog logger</returns>
        public static Logger ConfigureLogger(string applicationName)
        {
            try
            {            
                
                var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithProperty("ApplicationName", applicationName)
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .WriteTo.File($"Logs/log-{DateTime.Now.ToString("yyyyMMdd")}.txt")
                .WriteTo.MySQL("Server=localhost;Database=packen;Uid=root;Pwd=;","Logs")
                .CreateLogger();
                
                return logger;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
