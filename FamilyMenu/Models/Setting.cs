using System;
using SQLite.Net.Attributes;

namespace FamilyMenu
{
	public class Setting
	{
		public Setting()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string key { get; set; }
		public string value { get; set; }
	}
}

