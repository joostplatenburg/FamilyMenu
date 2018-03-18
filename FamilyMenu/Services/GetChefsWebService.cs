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
    // http://www.platenburg.eu/php/FamilyMenu/getchefs.json.php

    public class GetChefsWebService
    {
          public GetChefsWebService()
        {
        }

        public async Task<Chef[]> GetChefsAsync()
        {
			Debug.WriteLine("Start GetChefsAsync()");

            var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri("http://www.platenburg.eu/php/FamilyMenu/");

            var command = "getchefs.json.php";

            var response = await client.GetAsync(command);

            var chefsJson = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine(chefsJson);

            var chefs = JsonConvert.DeserializeObject<Chefs>(chefsJson);

            return chefs.chefs;
        }
    }

}
