using ETL.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL
{
    internal class APIData
    {
        public APIData(List<string> countries, int countOfTasksInput)
        {
            listOfCountries = countries;
            this.countOfTasksInput = countOfTasksInput;
        }

        ResponseParser responseParser;

        private List<string> listOfCountries = new List<string>();
        private readonly int countOfTasksInput;
        private List<Institute> SetTasks()
        {
            responseParser = new();

            SemaphoreSlim semaphoreSlim = new SemaphoreSlim(countOfTasksInput);
            HttpClient client = new HttpClient();

            List<Task<string>> tasks = new List<Task<string>>();
            foreach (var item in listOfCountries)
            {
                tasks.Add(MakeRequest(client, $@"http://universities.hipolabs.com/search?country={item}", semaphoreSlim));
            }

            return responseParser.Parse(tasks);
        }

        private async Task<string> MakeRequest(HttpClient client, string url, SemaphoreSlim semaphoreSlim)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                var response = await client.GetAsync(url).Result.Content.ReadAsStringAsync();
                return response;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public List<Institute> GetData()
        {
            return SetTasks();
        }
    }

    internal sealed class ResponseParser
    {
        internal List<Institute> Parse(List<Task<string>> responseList) 
        {
            var result = new List<Institute>();

            foreach (var response in responseList)
            {
                var readedJson = JArray.Parse(response.Result);

                foreach (var item in readedJson)
                {
                    var stroke = new Institute();
                    stroke.CountryName = item["country"]?.ToString();
                    stroke.Name = item["name"]?.ToString();
                    stroke.WebPage = GetWebPages(item["web_pages"]?.ToString());

                    if (readedJson.Any())
                        result.Add(stroke);
                }
            }

            return result;
        }

        private string GetWebPages(string webPages) 
        {
            if (string.IsNullOrEmpty(webPages))
                return string.Empty;

            var parsedPages = JArray.Parse(webPages).ToList();

            return string.Join(";", parsedPages.Select(a => a).ToList());
        }
    }
}
