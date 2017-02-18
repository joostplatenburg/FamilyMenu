using System;
using System.ComponentModel;
using SQLite.Net.Attributes;
using Xamarin.Forms;

namespace FamilyMenu
{
	public class DieetOmschrijving
    {
		public string dieetOmschrijving { get; set; }
	}

	public class DieetOmschrijvingen
	{
		public DieetOmschrijving[] dieetOmschrijvingen { get; set; }
	}
}
