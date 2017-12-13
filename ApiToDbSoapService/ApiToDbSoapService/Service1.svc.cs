using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ApiToDbSoapService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        // http://fil.nrk.no/yr/viktigestader/verda.txt
        private const string Uri = "http://www.yr.no/sted/Danmark/Sjælland/Roskilde/varsel.xml";

        private const string ConnectionString =
            "Server=tcp:lyssensor-server.database.windows.net,1433;Initial Catalog=LysSensorDB;Persist Security Info=False;User ID=gruppe6;Password=Hejmeddig1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public void InsertWeatherDataIntoDb()
        {
            const string sql =
                "insert into WeatherData([WeatherDate], [Temperature], [Cloudiness], [Sunrise]) values (@date, @temperature, @cloudiness, @sunrise)";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(Uri).Result;

                if (response.IsSuccessStatusCode)
                {
                    string xmlString = GetProductAsync(client, Uri).Result;
                    IList<WeatherData> weather = GetWeatherFromXml(xmlString);
                    Console.WriteLine(string.Join("\n", weather));

                    foreach (var weatherData in weather)
                    {
                        string date = weatherData.From;
                        int temperature = weatherData.Temperature;
                        string cloudiness = weatherData.Cloudiness;
                        string sunrise = weatherData.Sunrise;

                        using (SqlConnection cnn = new SqlConnection(ConnectionString))
                        {
                            cnn.Open();
                            using (SqlCommand cmd = new SqlCommand(sql, cnn))
                            {
                                cmd.CommandText = sql;

                                cmd.Parameters.AddWithValue("@date", date);
                                cmd.Parameters.AddWithValue("@temperature", temperature);
                                cmd.Parameters.AddWithValue("@cloudiness", cloudiness);
                                cmd.Parameters.AddWithValue("@sunrise", sunrise);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("HttpResponse status code:" + (int)response.StatusCode + " " + response.ReasonPhrase);
                }
            }
        }

        private static async Task<string> GetProductAsync(HttpClient client, string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            throw new IOException("HttpResponse " + response.IsSuccessStatusCode + " " + response.ReasonPhrase);
        }

        private static IList<WeatherData> GetWeatherFromXml(string xmlString)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);

            XmlNodeList namesXmlList = xmlDocument.GetElementsByTagName("time");
            IList<WeatherData> weather = new List<WeatherData>();

            XmlNodeList nameSunriseXmlList = xmlDocument.GetElementsByTagName("sun");
            XmlNode nameSunriseNode = nameSunriseXmlList[0];
            string sunrise = nameSunriseNode.Attributes["rise"].Value;

            for (int i = 0; i < namesXmlList.Count; i++)
            {
                XmlNode namesNode = namesXmlList[i];
                string from = namesNode.Attributes["from"].Value;
                string to = namesNode.Attributes["to"].Value;

                if (from.Contains("12:00") && to.Contains("18:00") || from.Contains("13:00") && to.Contains("19:00"))
                {
                    XmlNode temperatureNode = namesNode.SelectSingleNode("temperature");
                    string temperatureString = temperatureNode.Attributes["value"].Value;
                    int temperature = int.Parse(temperatureString);

                    XmlNode cloudinessNode = namesNode.SelectSingleNode("symbol");
                    string cloudiness = cloudinessNode.Attributes["name"].Value;

                    WeatherData w = new WeatherData()
                    {
                        From = from,
                        To = to,
                        Temperature = temperature,
                        Cloudiness = cloudiness,
                        Sunrise = sunrise
                    };
                    weather.Add(w);
                }
            }
            return weather;
        }
    }
}
