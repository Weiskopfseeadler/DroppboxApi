using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DroppboxApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("teST1");
            CreateWebHostBuilder(args).Build().Run();
            System.Console.WriteLine("TEST2");
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
                
    }
}
