using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace FamilyMenu
{
	public class MenuOmschrijving
    {
		public string Omschrijving { get; set; }
	}

	public class MenuOmschrijvingen
	{
		public MenuOmschrijving[] menuOmschrijvingen { get; set; }
	}
}
