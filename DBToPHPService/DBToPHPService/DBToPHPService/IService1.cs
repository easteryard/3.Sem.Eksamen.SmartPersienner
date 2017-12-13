using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DBToPHPService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "lightvalues")]
        IList<LightValues> GetLightValue();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "weatherDate/")]
        IList<WeatherData> GetWeatherDate();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "temperature/")]
        IList<WeatherData> GetTemperature();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "cloudiness/")]
        IList<WeatherData> GetCloudiness();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "sunrise/")]
        IList<WeatherData> GetSunrise();
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class LightValues
    {
        [DataMember]
        public int ID;

        [DataMember]
        public int Lightvalue;

    }

    [DataContract]
    public class WeatherData
    {
        [DataMember]
        public string WeatherDate;

        [DataMember]
        public int Temperature;

        [DataMember]
        public string Cloudiness;

        [DataMember]
        public string Sunrise;
    }
}
