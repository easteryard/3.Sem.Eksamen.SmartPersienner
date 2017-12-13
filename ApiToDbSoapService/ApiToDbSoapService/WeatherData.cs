using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiToDbSoapService
{
    public class WeatherData
    {
        public string From { get; set; }
        public string To { get; set; }
        public int Temperature { get; set; }
        public string Cloudiness { get; set; }
        public string Sunrise { get; set; }

        public override string ToString()
        {
            return $"{nameof(From)}: {From}, {nameof(To)}: {To}, {nameof(Temperature)}: {Temperature}, {nameof(Cloudiness)}: {Cloudiness}, {nameof(Sunrise)}: {Sunrise}";
        }
    }
}