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
    public class InsertMenuEntryWebService
    {
        string InsertStm = @"insert.php?id={0}&dt={1}&kk='{2}'&om='{3}'&di='{4}'&dn='{5}'";

        public InsertMenuEntryWebService() { }

        public async Task<Boolean> InsertMenuAsync(MenuEntry insertedMenu)
        {
            Debug.WriteLine("Start InsertMenuAsync(" + insertedMenu.Datum +")");

            var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri("http://www.platenburg.eu/php/FamilyMenu/");

            var command = string.Format(InsertStm,
                    insertedMenu.ID, insertedMenu.Datum,
                    insertedMenu.Chef, insertedMenu.Omschrijving, insertedMenu.Dieet,
                    App.DeviceName);

			Debug.WriteLine ("Command: " + command);

            var response = await client.GetAsync(command);

            var returnVal = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine(returnVal);

            return true;
        }
    }

}
