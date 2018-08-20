using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace FamilyMenu
{
    public class CurrentWeek
    {
        [JsonProperty(PropertyName = "startdate")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "week")]
        public ObservableCollection<Day> Days { get; set; }

        public CurrentWeek()
        {
        }
    }

    public class Day
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "datum")]
        public DateTime? Datum { get; set; }

        [JsonProperty(PropertyName = "chef")]
        public string Chef { get; set; }

        [JsonProperty(PropertyName = "omschrijving")]
        public string Omschrijving { get; set; }

        [JsonProperty(PropertyName = "dieet")]
        public string Dieet { get; set; }

        [JsonProperty(PropertyName = "aantaldieet")]
        public string AantalDieet { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime? Date { get; set; }

        [JsonProperty(PropertyName = "time")]
        public TimeSpan? time { get; set; }

        [JsonProperty(PropertyName = "device")]
        public string Device { get; set; }
    }
}
