using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace FamilyMenu.Services
{
    // http://www.platenburg.eu/php/FamilyMenu/getweek.json.php?datum=2015-07-18

    public class FamilyMenuServices
    {
        HttpClient client;

        string InsertStm = @"insert.php?id={0}&dt={1}&kk='{2}'&om='{3}'&di='{4}'&dn='{5}'";
        string updateChefStm = @"updateChef.php?id={0}&name={1}&dn='{2}'";
        string deleteChefStm = @"deleteChef.php?id={0}";


        public FamilyMenuServices() 
        {
            Debug.WriteLine("JAP001 - FamilyMenuServices()");

            client = new HttpClient();

            client.BaseAddress = new Uri("http://www.platenburg.eu/php/FamilyMenu/");
        }

        public async Task<Week> GetWeekAsync(string startdatum)
        {
            Debug.WriteLine(string.Format("JAP001 - FamilyMenuServices.GetWeekAsync({0})", startdatum));

            try
            {
                var service = string.Format("getweek.json.php?datum={0}", startdatum);

                Debug.WriteLine(string.Format("JAP001 - > Request: {0}{1}", client.BaseAddress, service));

                var response = client.GetAsync(service).Result;

                if (response.StatusCode == HttpStatusCode.Continue ||
                    response.StatusCode == HttpStatusCode.Accepted ||
                    response.StatusCode == HttpStatusCode.OK)
                {
                    var weekJson = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine("JAP001 - > Response: " + weekJson);

                    var week = JsonConvert.DeserializeObject<Week>(weekJson);

                    return week;
                }

                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("JAP001 - Response: {0}", ex.Message));

                return null;
            }
        }

        public async Task<ObservableCollection<Chef>> GetChefsAsync()
        {
            Debug.WriteLine("JAP001 - FamilyMenuServices.GetChefsAsync()");

            try
            {
                var service = string.Format("getchefs.json.php");

                Debug.WriteLine(string.Format("JAP001 - > Request: {0}{1}", client.BaseAddress, service));

                var response = await client.GetAsync(service);

                if (response.StatusCode == HttpStatusCode.Continue ||
                    response.StatusCode == HttpStatusCode.Accepted ||
                    response.StatusCode == HttpStatusCode.OK)
                {
                    var json = response.Content.ReadAsStringAsync().Result;

                    Debug.WriteLine("JAP001 - " + json);

                    var chefs = JsonConvert.DeserializeObject<Chefs>(json);

                    return chefs.chefs;
                }

                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("JAP001 - {0}", ex.Message));

                return null;
            }
        }

        public async Task<Boolean> UpdateMenuAsync(MenuEntry updatedMenu)
        {
            Debug.WriteLine(string.Format("JAP001 - FamilyMenuServices.UpdateMenuAsync({0})", updatedMenu.Datum));

            try
            {
                var deviceName = Xamarin.Essentials.DeviceInfo.Name;

                var dt = DateTime.Parse(updatedMenu.Datum);
                var datum = string.Format("{0}-{1:D2}-{2:D2}", dt.Year, dt.Month, dt.Day);

                //www.platenburg.eu/php/FamilyMenu/update.php?dt=2018-10-13&kk='Choose a chef'&om='Tomatensoepjes stokbrood'&dn='DESKTOP-PVLPC2P'

                var service = string.Format("update.php?dt={0}&kk='{1}'&om='{2}'&dn='{3}'", datum, updatedMenu.Chef, updatedMenu.Omschrijving, deviceName);

                Debug.WriteLine(string.Format("JAP001 - > Request: {0}{1}", client.BaseAddress, service));

                var response = await client.GetAsync(service);

                if (response.StatusCode == HttpStatusCode.Continue ||
                    response.StatusCode == HttpStatusCode.Accepted ||
                    response.StatusCode == HttpStatusCode.OK)
                {
                    var returnval = response.Content.ReadAsStringAsync().Result;

                    Debug.WriteLine("JAP001 - " + returnval);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("JAP001 - {0}", ex.Message));

                return false;
            }
        }

        public async Task<string> GetHighestIDAsync()
        {
            Debug.WriteLine("JAP001 - FamilyMenuServices.GetHighestIDAsync()");

            var service = "getHighestID.php";

            Debug.WriteLine(string.Format("JAP001 - > Request: {0}{1}", client.BaseAddress, service));

            var response = await client.GetAsync(service);

            var highestID = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine(highestID);

            return highestID;
        }

        public async Task<Boolean> InsertMenuAsync(MenuEntry insertedMenu)
        {
            Debug.WriteLine(string.Format("JAP001 - FamilyMenuServices.InsertMenuAsync({0})", insertedMenu.Datum));

            var service = string.Format(InsertStm,
                    insertedMenu.ID, insertedMenu.Datum,
                    insertedMenu.Chef, insertedMenu.Omschrijving, 
                    App.DeviceName);

            Debug.WriteLine(string.Format("JAP001 - > Request: {0}{1}", client.BaseAddress, service));

            var response = await client.GetAsync(service);

            var returnVal = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine(returnVal);

            return true;
        }

        public async Task<MenuOmschrijving[]> GetMenuOmschrijvingenAsync()
        {
            Debug.WriteLine("JAP001 - FamilyMenuServices.GetMenuOmschrijvingenAsync()");

            var service = "getMenuOmschrijvingen.json.php";

            Debug.WriteLine(string.Format("JAP001 - > Request: {0}{1}", client.BaseAddress, service));

            var response = await client.GetAsync(service);

            var menuOmschrijvingenJson = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine(menuOmschrijvingenJson);

            var menuOmschrijvingen = JsonConvert.DeserializeObject<MenuOmschrijvingen>(menuOmschrijvingenJson);

            return menuOmschrijvingen.menuOmschrijvingen;
        }
    }
}
