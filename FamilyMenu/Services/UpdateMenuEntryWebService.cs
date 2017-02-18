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
    public class UpdateMenuEntryWebService
    {
        string UpdateStm = @"update.php?id={0}&dt={1}&kk='{2}'&om='{3}'&di='{4}'&dn='{5}'";

        public UpdateMenuEntryWebService() { }

        public async Task<Boolean> UpdateMenuAsync(MenuEntry updatedMenu)
        {
            Debug.WriteLine("Start UpdateMenuAsync(" + updatedMenu.Datum +")");

            var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri("http://www.platenburg.eu/php/FamilyMenu/");

            var command = string.Format(UpdateStm,
                    updatedMenu.ID, updatedMenu.Datum,
                    updatedMenu.Chef, updatedMenu.Omschrijving, updatedMenu.Dieet,
                    App.DeviceName);

			Debug.WriteLine ("Command: " + command);

            var response = await client.GetAsync(command);

            var returnVal = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine(returnVal);

            return true;

        }
    }
//	Date: 2015-10-02<br>
//	Time: 18:15:56<br>
//	ID: 331<br>
//	Datum: 2015-09-26<br>
//	Chef: 'Jacqueline'<br>
//	Omschrijving: 'Tomatensoep'<br>
//	Dieet: ''<br>
//	Device: 'b9c19135a60bf705'<br>
//	UPDATE MenuEntry SET Date='2015-10-02', Time='18:15:56', Datum='2015-09-26', Chef='Jacqueline', Omschrijving='Tomatensoep', Dieet='', DeviceName='b9c19135a60bf705' 
//		WHERE Datum='2015-09-26'<br>
//		INSERT INTO MenuEntry(Date, Time, Datum, Chef, Omschrijving, Dieet, DeviceName) VALUES('2015-10-02', '18:15:56', '2015-09-26', 'Jacqueline', 'Tomatensoep', '', 'b9c19135a60bf705')<br>
//		De insert levert een count op van  en is dus mislukt<br>
//		Script is volledig doorlopen!<br>
//
//		http://www.platenburg.eu/php/FamilyMenu/update.php?id=331&dt=2015-09-26&kk='Jacqueline'&om='Tomatensoep'&di=''&dn='b9c19135a60bf705'
}
