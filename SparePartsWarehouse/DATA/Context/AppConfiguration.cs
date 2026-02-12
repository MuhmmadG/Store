using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.IO;
using System;
namespace SparePartsWarehouse.DATA.Context
{
    public static class AppConfiguration
    {
        private static IConfigurationRoot configuration;

        static AppConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) // مسار التشغيل
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();
        }

        public static string GetConnectionString(string name = "DefaultConnection")
        {
            return configuration.GetConnectionString(name);
        }
    }
}
