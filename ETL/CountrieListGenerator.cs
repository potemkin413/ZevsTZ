using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL
{
    internal class CountrieListGenerator
    {
        Countries countries;

        public void GetRandomCountries(ref List<string> entryList) 
        {
            countries = new();
            countries.GetListOfCounties(ref entryList);
        }
    }

    internal class Countries
    {
        internal void GetListOfCounties(ref List<string> entryList) 
        {
            entryList = RandomCountries(ReadAllCountries());
        }

        private string[] ReadAllCountries() => File.ReadAllText("E:\\projects\\Zevs\\Zevs\\Countries.txt").Split(',');

        private List<string> RandomCountries(string[] readedFile)
        {
            var result = new List<string>();
            var alreadyAdded = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                var randomIndex = GetRandomIndex(readedFile.Length);
                while (!alreadyAdded.Contains(randomIndex))
                {
                    randomIndex = GetRandomIndex(readedFile.Length);
                    alreadyAdded.Add(randomIndex);
                }
                result.Add(readedFile[randomIndex].Replace("\r\n", "").Replace(" ", "+"));
            }

            return result;
        }

        private int GetRandomIndex(int lenght) => new Random().Next(lenght);
    }
}
