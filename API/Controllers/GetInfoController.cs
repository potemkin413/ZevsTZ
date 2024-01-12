using API.ContextDB;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetInfoController
    {
        [HttpGet]
        public string Get([FromQuery(Name = "name")] string name = "", [FromQuery(Name = "countryName")] string countryName = "")
        {
            using (var instituteContext = new InstituteContext())
            {
                IQueryable<Institute> allInstitues = instituteContext.Institute;
                if (!string.IsNullOrEmpty(name))
                {
                    allInstitues = allInstitues.Where(a => a.Name == name);
                }
                if (!string.IsNullOrEmpty(countryName))
                {
                    allInstitues = allInstitues.Where(a => a.CountryName == countryName);
                }

                return JsonConvert.SerializeObject(allInstitues.Select(a => new 
                { 
                    Name = a.Name,
                    CountryName = a.CountryName,
                    WebPage = a.WebPage
                }).GroupBy(a => a.Name));
            }
        }
    }
}
