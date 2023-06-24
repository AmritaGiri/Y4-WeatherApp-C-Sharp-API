using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace Weather_CA1
{
    public partial class WeatherForecast : Form
    {
        double lon;
        double lat;
        public WeatherForecast()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        string APIKey = "bb0d2d1cce16bf3b5fe4a284d8ce9b0f"; // API Key Value from our Openweathermap Website 

        //so when we click on the search button these functions will be executed
        // these functions will download all the weather information and place it in its right place
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            getWeather();
            getForecast(); 
        } 

            //since we are accessing them in our class
            // then just passing them in our Forecast function 
            // creating global variables lon, lat
           

        // we created a Function called getWeather
        void getWeather()
        {
            // creating a WebClient for downloading the weather information from openweathermap
            using (WebClient web = new WebClient())
            {
                // first we create a complete URL for downloading the weather information 
                // TBCity is the label we have for the text box that will search for city names
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", TBCity.Text, APIKey);

                // Now we are downloading the weather information into JSON objects 
                var json = web.DownloadString(url);

                //so once all the data is coverted to JSON we are gonna Deserialize this into our class WeatherInfo
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                //once we have all the information in Info we use it to update our labels we made while doing designer forms 
               // updating our label values with the current weather values
                picIcon.ImageLocation = "https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png"; 
                labCondition.Text = Info.weather[0].main;
                labDetails.Text = Info.weather[0].description;
                
                // instead of just displaying that sec we can convert this into convertDateTime
                // further converting to Time String for only time date not needed
                labSunset.Text = convertDateTime(Info.sys.sunset).ToShortTimeString();
                labSunrise.Text = convertDateTime(Info.sys.sunrise).ToShortTimeString();

                labWindSpeed.Text = Info.wind.speed.ToString();
                labPressure.Text = Info.main.pressure.ToString();

                // when we get the weather of a specific city
                // we extract the lon lat 
                lon = Info.coord.lon;
                lat = Info.coord.lat;
                // so when we search for a paticular city we get its longitude and latitude from here 
            }
        }

        // function for converting sunrise and sunset into current day time in proper format
        DateTime convertDateTime(long sec)
        {
            // (1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)
            //from 1970 
            // first month 
            //first day
            // zero hours
            // zero minutes
            // zero seconds
            // zero millisecs

            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();

            // in 1970 like this time above we are gonna add these secs
            // so this will return the exact current time that is represented by these secs
            // again converting this to the local time and returning the value in day
            day = day.AddSeconds(sec).ToLocalTime();

            return day;
        }

        // we created a Function called getForecast
        void getForecast()
        {
            using (WebClient web = new WebClient())
            {
                // creating a WebClient for downloading the forecast information from openweathermap
                string url = string.Format("https://api.openweathermap.org/data/3.0/onecall?lat={0}&lon={1}&appid={2}", lat, lon, APIKey);

                // Now we are downloading the forecast information into JSON objects 
                var json = web.DownloadString(url);


                //so once all the data is coverted to JSON we are gonna Deserialize this into our class ForeInfo
                ForeInfo.forecastInfo forecastInfo = JsonConvert.DeserializeObject<ForeInfo.forecastInfo>(json);

                // User Control (Windows Form from new item)
                // using user forms to display a child form under a parent form 
                ForecastUC FUC;
                for (int i = 0; i < 8; i++)
                {
                    FUC = new ForecastUC();
                    FUC.picWeatherIcon.ImageLocation = "https://openweathermap.org/img/w/" + forecastInfo.daily[i].weather[0].icon + ".png";
                    FUC.labMainWeather.Text = forecastInfo.daily[i].weather[0].main;
                    FUC.labWeatherDescription.Text = forecastInfo.daily[i].weather[0].description;
                    FUC.labDT.Text = convertDateTime(forecastInfo.daily[i].dt).DayOfWeek.ToString();

                    FLP.Controls.Add(FUC);
                }
            }
        }

        //opens the Gmail Form for sending out emails 
        private void button1_Click(object sender, EventArgs e)
        {
            Gmail form = new Gmail();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
