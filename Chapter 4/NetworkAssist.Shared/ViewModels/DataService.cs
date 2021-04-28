using Newtonsoft.Json;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnoBookRail.Common.Network;

namespace NetworkAssist.ViewModels
{
    public class DataService
    {
        private static readonly HttpClient _http = new HttpClient();

        private static readonly Lazy<DataService> ds =
            new Lazy<DataService>(() => new DataService());

        private static readonly Lazy<Stations> stations =
            new Lazy<Stations>(() => new Stations());

        public static DataService Instance => ds.Value;

        private DataService() { }

        public static string WebApiDomain
        {
            get
            {
                //#if __ANDROID__
                //                return "https://10.0.2.2:44302";
                //#else
                //                return "https://localhost:44302";
                //#endif
                return "https://unobookrail.azurewebsites.net";
            }
        }

        public List<Station> GetAllStations()
        {
            return stations.Value.GetAll().OrderBy(s => s.Name).ToList();
        }

        // Initial version - using data from the device.
        ////public async Task<Arrivals> GetArrivalsForStationAsync(int stationId)
        ////{
        ////    return await Task.FromResult(stations.Value.GetNextArrivals(stationId));
        ////}

        // Second version - connecting to remote data.
        ////public async Task<Arrivals> GetArrivalsForStationAsync(int stationId)
        ////{
        ////    var url = $"{WebApiDomain}/stations/?stationid={stationId}";

        ////    var rawJson = await _http.GetStringAsync(url);

        ////    return JsonConvert.DeserializeObject<Arrivals>(rawJson);
        ////}

        // Final version - including retrying after transient errors.
        public async Task<Arrivals> GetArrivalsForStationAsync(int stationId)
        {
            var url = $"{WebApiDomain}/stations/?stationid={stationId}";

            var policy = HttpPolicyExtensions
                            .HandleTransientHttpError()
                            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(attempt, 2)));

            using (var response = await policy.ExecuteAsync(async () => await _http.GetAsync(url)))
            {
                if (response.IsSuccessStatusCode)
                {
                    var rawJson = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Arrivals>(rawJson);
                }
            }

            return default;
        }
    }

}
