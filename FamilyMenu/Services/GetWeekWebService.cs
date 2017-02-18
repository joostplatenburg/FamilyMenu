using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace FamilyMenu.Services
{
    // http://www.platenburg.eu/php/FamilyMenu/getweek.json.php?datum=2015-07-18

    public class GetWeekWebService
    {
        public GetWeekWebService() { }

        public async Task<MenuEntry[]> GetWeekAsync(string startdatum)
        {
            Debug.WriteLine("Start GetWeekAsync("+startdatum+")");

            var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri("http://www.platenburg.eu/php/FamilyMenu/");

            var command = "getweek.json.php?datum=" + startdatum;

            var response = await client.GetAsync(command);

            var weekJson = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine(weekJson);

            var week = JsonConvert.DeserializeObject<Week>(weekJson);

            return week.week;

        }
    }

}
