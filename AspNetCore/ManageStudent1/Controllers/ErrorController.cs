using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageStudent1.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{StatusCode}")]
        public IActionResult Index(int StatusCode)
        {
            string message = string.Empty;
            switch (StatusCode)
            {
                
                case 404:
                    {
                        message = "Sorry, The ressource you requested could not be found";
                    }break;
            }
            return View("NotFound",message);
        }
    }
}
