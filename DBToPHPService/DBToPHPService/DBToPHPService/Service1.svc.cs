using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DBToPHPService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        private const string ConnectionString =
            "Server=tcp:lyssensor-server.database.windows.net,1433;Initial Catalog=LysSensorDB;Persist Security Info=False;User ID=gruppe6;Password=Hejmeddig1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public IList<LightValues> GetLightValue()
        {
            string selectStr = "select Lightvalue from Lightsensor";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectStr, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@selectStr", selectStr);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<LightValues> lightValueList = new List<LightValues>();
                        while (reader.Read())
                        {
                            LightValues lightValues = ReadLightValues(reader);
                            lightValueList.Add(lightValues);
                        }
                        return lightValueList;
                    }
                }
            }
        }

        public IList<WeatherData> GetWeatherDate()
        {
            string selectStr = "select WeatherDate from WeatherData";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectStr, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("selectStr", selectStr);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<WeatherData> weatherDateValueList = new List<WeatherData>();
                        while (reader.Read())
                        {
                            WeatherData weatherDateValues = ReadWeatherDateValues(reader);
                            weatherDateValueList.Add(weatherDateValues);
                        }
                        return weatherDateValueList;
                    }
                }
            }
        }

        public IList<WeatherData> GetTemperature()
        {
            string selectStr = "select Temperature from WeatherData";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectStr, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("selectStr", selectStr);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<WeatherData> weatherDateValueList = new List<WeatherData>();
                        while (reader.Read())
                        {
                            WeatherData weatherDateValues = ReadTemperatureValues(reader);
                            weatherDateValueList.Add(weatherDateValues);
                        }
                        return weatherDateValueList;
                    }
                }
            }
        }

        public IList<WeatherData> GetCloudiness()
        {
            string selectStr = "select Cloudiness from WeatherData";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectStr, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("selectStr", selectStr);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<WeatherData> weatherDateValueList = new List<WeatherData>();
                        while (reader.Read())
                        {
                            WeatherData weatherDateValues = ReadCloudinessValues(reader);
                            weatherDateValueList.Add(weatherDateValues);
                        }
                        return weatherDateValueList;
                    }
                }
            }
        }

        public IList<WeatherData> GetSunrise()
        {
            string selectStr = "select Sunrise from WeatherData";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectStr, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("selectStr", selectStr);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<WeatherData> weatherDateValueList = new List<WeatherData>();
                        while (reader.Read())
                        {
                            WeatherData weatherDateValues = ReadSunriseValues(reader);
                            weatherDateValueList.Add(weatherDateValues);
                        }
                        return weatherDateValueList;
                    }
                }
            }
        }


        private static LightValues ReadLightValues(IDataRecord reader)
        {
            int lightValue = reader.GetInt32(0);
            LightValues lightValues = new LightValues
            {
                Lightvalue = lightValue
            };
            return lightValues;
        }

        private static WeatherData ReadWeatherDateValues(IDataRecord reader)
        {
            string weatherDateValue = reader.GetString(0);
            WeatherData weatherDataValues = new WeatherData
            {
                WeatherDate = weatherDateValue
            };
            return weatherDataValues;
        }

        private static WeatherData ReadTemperatureValues(IDataRecord reader)
        {
            int temperatureValue = reader.GetInt32(0);
            WeatherData temperatureValues = new WeatherData
            {
                Temperature = temperatureValue
            };
            return temperatureValues;
        }

        private static WeatherData ReadCloudinessValues(IDataRecord reader)
        {
            string cloudinessValue = reader.GetString(0);
            WeatherData cloudinessValues = new WeatherData
            {
                Cloudiness = cloudinessValue
            };
            return cloudinessValues;
        }

        private static WeatherData ReadSunriseValues(IDataRecord reader)
        {
            string sunriseValue = reader.GetString(0);
            WeatherData sunriseValues = new WeatherData
            {
                Sunrise = sunriseValue
            };
            return sunriseValues;
        }
    }
}
