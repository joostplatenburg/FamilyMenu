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

	public class GetMenuOmchrijvingenWebService
    {
		public GetMenuOmchrijvingenWebService() { }

        public async Task<MenuOmschrijving[]> GetMenuOmschrijvingenAsync()
        {
			Debug.WriteLine("Start GetMenuOmschrijvingenAsync()");

            var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri("http://www.platenburg.eu/php/FamilyMenu/");

            var command = "getMenuOmschrijvingen.json.php";

            var response = await client.GetAsync(command);

            var menuOmschrijvingenJson = response.Content.ReadAsStringAsync().Result;

			Debug.WriteLine(menuOmschrijvingenJson);

			var menuOmschrijvingen = JsonConvert.DeserializeObject<MenuOmschrijvingen>(menuOmschrijvingenJson);

            return menuOmschrijvingen.menuOmschrijvingen;
        }
    }

}
