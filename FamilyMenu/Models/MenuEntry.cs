using System;
using SQLite.Net.Attributes;
using Xamarin.Forms;
using System.ComponentModel;

namespace FamilyMenu
{
	public class MenuEntry
    {
		[PrimaryKey]
		public int ID { get; set; }
		public string Datum {	get ; set; }
		public string Chef { get; set; }
		public string Omschrijving { get; set; }
		public String Date { get; set; }
		public String Time { get; set; }
		public String DeviceName { get; set; }
		public string Dieet { get; set; }
		public string AantalDieet { get; set; }
	}

    public class Week
    {
        public string startdate { get; set; }
        public MenuEntry[] week { get; set; }
    }

    public class Rootobject
    {
        public Week days { get; set; }
    }

}
