using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_CA1
{
    // class for handling the forecast data 
    internal class ForeInfo
    {
        // everything in the JSON format(See the APIdocs) that has data in curly brackets 
        // is actually a class
        // like temp, weather, daily
        // since C# is case-senstive we stuck to the naming accordingly from the JSON
        public class temp
        {
            public double day { get; set; }
        }

        public class weather
        {
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
        public class daily
        {
            public long dt { get; set; } // date time for the day 
            public temp temp { get; set; }
            public List<weather> weather { get; set; } // list of objects in weather class
        }

        public class forecastInfo
        {
            public List<daily> daily { get; set; }
        }
    }
}
