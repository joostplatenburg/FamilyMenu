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

        public FamilyMenuServices() 
        {
            Debug.WriteLine("Start GetWeekWebService()");

            client = new HttpClient();

            client.BaseAddress = new Uri("http://www.platenburg.eu/php/FamilyMenu/");
        }

        public async Task<Week> GetWeekAsync(string startdatum)
        {
            Debug.WriteLine("Start GetWeekAsync(" + startdatum + ")");

            try
            {
                var service = string.Format("getweek.json.php?datum={0}", startdatum);

                Debug.WriteLine("JAP001 - " + client.BaseAddress + service);

                var response = client.GetAsync(service).Result;

                if (response.StatusCode == HttpStatusCode.Continue ||
                    response.StatusCode == HttpStatusCode.Accepted ||
                    response.StatusCode == HttpStatusCode.OK)
                {
                    var weekJson = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine(weekJson);

                    var week = JsonConvert.DeserializeObject<Week>(weekJson);

                    return week;
                }

                return null;
            }
            catch (System.Net.WebException)
            {
                Debug.WriteLine(HttpStatusCode.InternalServerError);

                return null;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ObservableCollection<Chef>> GetChefsAsync()
        {
            Debug.WriteLine("Start GetChefsAsync()");

            try
            {
                var service = string.Format("getchefs.json.php");

                Debug.WriteLine("JAP001 - " + client.BaseAddress + service);

                var response = await client.GetAsync(service);

                if (response.StatusCode == HttpStatusCode.Continue ||
                    response.StatusCode == HttpStatusCode.Accepted ||
                    response.StatusCode == HttpStatusCode.OK)
                {
                    var json = response.Content.ReadAsStringAsync().Result;

                    Debug.WriteLine(json);

                    var chefs = JsonConvert.DeserializeObject<Chefs>(json);

                    return chefs.chefs;
                }

                return null;
            }
            catch (System.Net.WebException)
            {
                Debug.WriteLine(HttpStatusCode.InternalServerError);

                return null;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<Boolean> UpdateMenuAsync(MenuEntry updatedMenu)
        {
            Debug.WriteLine("Start UpdateMenuAsync(" + updatedMenu.Datum + ")");

            try
            {
                var deviceName = Xamarin.Essentials.DeviceInfo.Name;

                //var datum = updatedMenu.Datum.Year + "-"
                                       //+ string.Format("{0:D2}", updatedMenu.Datum.Month) + "-"
                                       //+ string.Format("{0:D2}", updatedMenu.Datum.Day);


                var service = string.Format("update.php?id={0}&dt={1}&kk='{2}'&om='{3}'&di='{4}'&dn='{5}'", updatedMenu.ID, updatedMenu.Datum,
                                            updatedMenu.Chef, updatedMenu.Omschrijving, updatedMenu.Dieet, deviceName);

                Debug.WriteLine("JAP001 - " + client.BaseAddress + service);

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
            catch (System.Net.WebException)
            {
                Debug.WriteLine(HttpStatusCode.InternalServerError);

                return false;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<string> GetHighestIDAsync()
        {
            Debug.WriteLine("Start GetHighestIDAsync()");

            var service = "getHighestID.php";

            var response = await client.GetAsync(service);

            var highestID = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine(highestID);

            return highestID;
        }

        public async Task<Boolean> InsertMenuAsync(MenuEntry insertedMenu)
        {
            Debug.WriteLine("Start InsertMenuAsync(" + insertedMenu.Datum + ")");

            var command = string.Format(InsertStm,
                    insertedMenu.ID, insertedMenu.Datum,
                    insertedMenu.Chef, insertedMenu.Omschrijving, insertedMenu.Dieet,
                    App.DeviceName);

            Debug.WriteLine("Command: " + client.BaseAddress + command);

            var response = await client.GetAsync(command);

            var returnVal = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine(returnVal);

            return true;
        }

        public async Task<MenuOmschrijving[]> GetMenuOmschrijvingenAsync()
        {
            Debug.WriteLine("Start GetMenuOmschrijvingenAsync()");

            var command = "getMenuOmschrijvingen.json.php";

            var response = await client.GetAsync(command);

            var menuOmschrijvingenJson = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine(menuOmschrijvingenJson);

            var menuOmschrijvingen = JsonConvert.DeserializeObject<MenuOmschrijvingen>(menuOmschrijvingenJson);

            return menuOmschrijvingen.menuOmschrijvingen;
        }
    }
}
