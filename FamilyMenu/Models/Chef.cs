using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace FamilyMenu
{
	public class Chef
	{
		public int ID { get; set; }
        public string Name { get; set; } 
        public string date { get; set; } 
        public string time { get; set; } 
        public string device { get; set; } 
	}

	public class Chefs
	{
		public ObservableCollection<Chef> chefs { get; set; }
	}
}

