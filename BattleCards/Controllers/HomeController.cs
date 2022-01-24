using BattleCards.ViewModels;
using SUS.MvcFramework;
using SUSHTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleCards.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse HomePage()
        {

            if(IsUserSignedIn())
            {
                return Redirect("/Cards/All");
            }
            
                return View();
            
        }
        
        
    }
}
