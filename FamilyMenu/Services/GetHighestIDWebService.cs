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

	public class GetHighestIDWebService
    {
		public GetHighestIDWebService()
        {
        }

        public async Task<string> GetHighestIDAsync()
        {
			Debug.WriteLine("Start GetHighestIDAsync()");

            var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri("http://www.platenburg.eu/php/FamilyMenu/");

            var command = "getHighestID.php";

            var response = await client.GetAsync(command);

            var highestID = response.Content.ReadAsStringAsync().Result;

			Debug.WriteLine(highestID);

			return highestID;
        }
    }

}
