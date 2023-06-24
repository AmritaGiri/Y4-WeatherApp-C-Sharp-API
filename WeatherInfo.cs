using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_CA1
{
    // everything in the JSON format(See the APIdocs) that has data in curly brackets 
    // is actually a class
    // like coord , weather , main , wind ,sys
    // since C# is case-senstive we stuck to the naming accordingly from the JSON
    internal class WeatherInfo
    {
        public class coord
        {
            public double lon { get; set; } // longitude 
            public double lat { get; set; } // latitude
        }

        public class weather
        {
            public string main { get; set; }    // main weather type (Cloud/Rain/Sun)
            public string description { get; set; } //dezcription of main weather type (few Cloud/less Rain/More Sun)
            public string icon { get; set; }  // appropriate icon for the weather type
        }

        public class main
        {
            public double temp { get; set; }  // temperature 
            public double pressure { get; set; }  // pressure (pascals)
            public double humidity { get; set; }  // humidity 
        }

        public class wind
        {
            public double speed { get; set; }  // speed of the wind (m/s)
        }
        public class sys
        {
            public long sunrise { get; set; }
            public long sunset { get; set; }
        }

           // so as we know that everything before the curly brackets is a class ,so all these are
          // actually an attribute to some other class which is not named here 
         // hence we just create a root class that contains all attributes now
        //basically a class that will contain all the above classes
        public class root
        {
            public coord coord { get; set; }
            public List<weather> weather { get; set; }
            public main main { get; set; }
            public wind wind { get; set; }
            public sys sys { get; set; }
        }
    }
}
