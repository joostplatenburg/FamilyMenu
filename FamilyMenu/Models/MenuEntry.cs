using System;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace FamilyMenu
{
	public class MenuEntry
    {
		public int ID { get; set; }
		public string Datum {	get ; set; }
		public string Chef { get; set; }
		public string Omschrijving { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public string DeviceName { get; set; }
		public string Dieet { get; set; }
		public string AantalDieet { get; set; }
	}

    public class Week
    {
        public string startdate { get; set; }
        public List<MenuEntry> week { get; set; }
    }

    public class Rootobject
    {
        public Week days { get; set; }
    }

}
