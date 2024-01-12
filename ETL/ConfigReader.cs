using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL
{
    public static class ConfigReader
    {
        public static string GetConnectionString() 
        {
            var readedFile = File.ReadAllText($@"{Directory.GetCurrentDirectory()}/ContextDB/Config.json");

            return JObject.Parse(readedFile)["ConnectionString"].ToString();
        }
    }
}
