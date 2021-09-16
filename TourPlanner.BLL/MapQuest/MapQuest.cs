using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TourPlanner.BLL.MapQuest.Models;


namespace TourPlanner.BLL.MapQuest
{
    public class MapQuest : IMapQuest
    {
        private readonly string _baseUrl;
        private readonly HttpClient _client;
        private readonly string _apiKey;
        private readonly string _filePath;
        private RouteWrapper _route;

        public MapQuest()
        {
            _client = new HttpClient();
            _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            _apiKey = ConfigurationManager.AppSettings["MapQuestKey"];
            _filePath = ConfigurationManager.AppSettings["ImagePath"];
        }

        public bool fetchData(string from, string to)
        {
            if (DoesLocationExist(from) && DoesLocationExist(to))
            {
                var directionsUrl = _baseUrl + "/directions/v2/route?key=" + _apiKey + "&from=" + from + "&to=" + to + "&outFormat=json&unit=k&locale=de_DE";
                var data = _client.GetStringAsync(directionsUrl).Result;
                var route = JsonConvert.DeserializeObject<RouteWrapper>(data);
                _route = route;

                return true;
            }
            else
            {
                return false;
            }
        }


        public string LoadImage()
        {
            var fileName =Guid.NewGuid().ToString();
            var fullFilePath = _filePath + fileName + ".jpg";

            var sessionId = _route.Route.SessionId;
            var staticUrl = _baseUrl + "/staticmap/v5/map?key=" + _apiKey + "&session=" + sessionId + "&size=300,200";
            var imageData = _client.GetByteArrayAsync(staticUrl).Result;
            File.WriteAllBytes(fullFilePath, imageData);

            return fullFilePath;
        }

        public bool DoesLocationExist(string location)
        {
            var task = Task.Run(() => _client.GetAsync(_baseUrl + "/geocoding/v1/address?key=" + _apiKey + "&location=" + location));
            task.Wait();

            var stringJsonResponse = task.Result.Content.ReadAsStringAsync().Result;

            JObject jSonResponse = JObject.Parse(stringJsonResponse);

            if (jSonResponse["results"]?[0]?["locations"]?.Count() > 1)
            {
                return true;
            }

            return false;
        }

        public int GetDistance()
        {
            return (int)_route.Route.Distance;
        }

    }
}
