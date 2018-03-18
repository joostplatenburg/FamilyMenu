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

	public class GetDieetmenuOmchrijvingenWebService
    {
		public GetDieetmenuOmchrijvingenWebService() { }

        public async Task<DieetOmschrijving[]> GetDieetmenuOmschrijvingenAsync()
        {
			Debug.WriteLine("Start GetDieetmenuOmschrijvingenAsync()");

            var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri("http://www.platenburg.eu/php/FamilyMenu/");

            var command = "getDieetmenuOmschrijvingen.json.php";

            var response = await client.GetAsync(command);

            var dieetOmschrijvingenJson = response.Content.ReadAsStringAsync().Result;

			Debug.WriteLine(dieetOmschrijvingenJson);

			var dieetOmschrijvingen = JsonConvert.DeserializeObject<DieetOmschrijvingen>(dieetOmschrijvingenJson);

			return dieetOmschrijvingen.dieetOmschrijvingen;
        }
    }

}
