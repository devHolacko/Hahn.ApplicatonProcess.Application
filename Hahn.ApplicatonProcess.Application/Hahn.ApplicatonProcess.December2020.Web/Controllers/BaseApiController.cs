using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        public BaseApiController(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(configuration.GetValue<string>("LogsFileName"), rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        public string LocalizationLanguage
        {
            get
            {
                bool language = Request.Headers.ContainsKey("lang");
                if (!language)
                    return "en";
                return Request.Headers["lang"];
            }
        }
    }
}
