using BattleCards.Controllers;
using SUS.MvcFramework;
using SUSHTTP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCards
{
   public class Program
    {
        public static async Task Main(string[] args)
        {
            

            await Host.RunAsync(new StartUp(), 80);
        }

        
    }
}
