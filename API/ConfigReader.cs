using Newtonsoft.Json.Linq;

namespace API
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
