using System;
using System.ComponentModel;
using SQLite.Net.Attributes;
using Xamarin.Forms;

namespace FamilyMenu
{
	public class Chef
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; } 
	}

	public class Chefs
	{
		public Chef[] chefs { get; set; }
	}
}

