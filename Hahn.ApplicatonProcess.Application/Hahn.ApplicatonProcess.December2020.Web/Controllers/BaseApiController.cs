using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
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
