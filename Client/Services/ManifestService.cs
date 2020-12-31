

using Data.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;


namespace Client.Services
{
    public sealed record ManifestService(MarsPhotoBrowserConfiguration marsPhotoBrowserConfiguration, HttpClient httpClient)
    : BaseService(marsPhotoBrowserConfiguration, httpClient)
    {
        public List<PhotoManifest> RoverPhotoManifests { get; set; }
        public async Task LoadManifestforRovers(IProgress<string> progress)
        {
            RoverPhotoManifests = new List<PhotoManifest>();
            RoverManifest roverManifest;

            foreach (var rover in MarsPhotoBrowserConfig.Rovers)
            {
                string roverManifestUrl = MarsPhotoBrowserConfig.NasaMarsApiUrl + $"manifests/{rover}?api_key={MarsPhotoBrowserConfig.NasaMarsApiKey}";
                progress.Report($"Loading data for Rover :{rover}....");

                roverManifest = await httpClient.GetFromJsonAsync<RoverManifest>(roverManifestUrl);

                // For testing purpose load the Manifest from local data folder.
                // using var response = await httpClient.GetAsync($"data/Manifest/{rover}.json");
                // roverManifest = await response.Content.ReadFromJsonAsync<RoverManifest>();

                RoverPhotoManifests.Add(roverManifest.PhotoManifest);
            }
        }
    }
}