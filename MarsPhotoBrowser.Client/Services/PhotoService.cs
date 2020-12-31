
using MarsPhotoBrowser.Data.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MarsPhotoBrowser.Client.Services
{
    public sealed record PhotoService(MarsPhotoBrowserConfiguration marsPhotoBrowserConfiguration, HttpClient httpClient)
    : BaseService(marsPhotoBrowserConfiguration, httpClient)
    {
        public async Task<LatestPhotos> GetLatestPhotosAsync(QueryConfiguration queryConfig)
        {
            string latestphotosUrl = MarsPhotoBrowserConfig.NasaMarsApiUrl + $"/rovers/{queryConfig.RoverName}/latest_photos?api_key={MarsPhotoBrowserConfig.NasaMarsApiKey}";
            return await httpClient.GetFromJsonAsync<LatestPhotos>(latestphotosUrl);
        }

        public async ValueTask<LatestPhotos> GetPhotosAsync(QueryConfiguration queryConfig)
        {
            StringBuilder builder = new StringBuilder(MarsPhotoBrowserConfig.NasaMarsApiUrl);
            builder.Append($"/rovers/{queryConfig.RoverName}/photos?api_key={MarsPhotoBrowserConfig.NasaMarsApiKey}");
            if (queryConfig.Sol > 0)
                builder.Append($"&sol={queryConfig.Sol}");
            builder.Append($"&page={queryConfig.PageNumber}");
            if (queryConfig.EarthDate.HasValue)
                builder.Append($"&earth_date={queryConfig.EarthDate.Value.ToString("yyyy-M-d")}");
            if (queryConfig.Camera != string.Empty)
                builder.Append($"&Camera={queryConfig.Camera}");
            return await httpClient.GetFromJsonAsync<LatestPhotos>(builder.ToString());
        }
    }
}
