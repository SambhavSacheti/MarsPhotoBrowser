using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Services;
using Data.Models;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Client.Components
{
    public partial class PhotoBrowseMain : ComponentBase
    {
        [Inject] PhotoService PhotoService { get; set; }
        [Inject] ManifestService ManifestService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private List<LatestPhoto> photos;

        Photo selectedSolManifest { get; set; }

        private long? previousSol;

        private long? nextSol;

        private PhotoManifest roverPhotoManifest;

        public string LoadingProgress { get; set; } = string.Empty;

        QueryConfiguration queryConfiguration;
        protected override async Task OnInitializedAsync()
        {
            photos = null;
            var progress = new Progress<string>(value =>
            {
                LoadingProgress = value;
                StateHasChanged();
            });
            await ManifestService.LoadManifestforRovers(progress).ContinueWith(t =>
            {
                roverPhotoManifest = ManifestService.RoverPhotoManifests[0];
                selectedSolManifest = roverPhotoManifest.Photos.Find(o => o.Sol == roverPhotoManifest.MaxSol);
                Console.WriteLine($"RoverManifests Total photos: {selectedSolManifest.TotalPhotos}, Sol: {selectedSolManifest.Sol} ");
                LoadingProgress = string.Empty;
            });
            queryConfiguration = new()
            {
                RoverName = roverPhotoManifest.Name,
                PageNumber = 1,
                Sol = selectedSolManifest.Sol
            };
            var result = await PhotoService.GetPhotosAsync(queryConfiguration);
            photos = result.photos;
            previousSol = (selectedSolManifest.Sol > 0)
                   ? roverPhotoManifest.Photos.FindLast(o => o.Sol < selectedSolManifest.Sol).Sol 
                   : null;
            nextSol = (selectedSolManifest.Sol < (roverPhotoManifest.MaxSol - 1))
                 ? roverPhotoManifest.Photos.Find(o => o.Sol > selectedSolManifest.Sol).Sol 
                 : null;
            Console.WriteLine($"previous sol: {previousSol.HasValue}");
            Console.WriteLine($"next sol: {nextSol.HasValue}");

        }

        private void BrowseByChanged(int value)
        {
            
        }
        private async Task PreviousSolButtonClickedAsync()
        {
            photos = null;
            nextSol = selectedSolManifest.Sol;
            selectedSolManifest = roverPhotoManifest.Photos.Find(o => o.Sol == previousSol);
          
            previousSol = (selectedSolManifest.Sol > 1)
                    ? roverPhotoManifest.Photos.FindLast(o => o.Sol < selectedSolManifest.Sol).Sol 
                    : null;

            queryConfiguration = new()
            {
                RoverName = roverPhotoManifest.Name,
                PageNumber = 1,
                Sol = selectedSolManifest.Sol,
            };
            var result = await PhotoService.GetPhotosAsync(queryConfiguration);

            photos = result.photos;
        }

        private async Task NextSolButtonClickedAsync()
        {
            photos = null;
            previousSol = selectedSolManifest.Sol;
            selectedSolManifest = roverPhotoManifest.Photos.Find(o => o.Sol == nextSol);
            nextSol = (selectedSolManifest.Sol < (roverPhotoManifest.MaxSol - 1))
                    ? roverPhotoManifest.Photos.Find(o => o.Sol > selectedSolManifest.Sol).Sol 
                    : null;
            queryConfiguration = new()
            {
                RoverName = roverPhotoManifest.Name,
                PageNumber = 1,
                Sol = selectedSolManifest.Sol,
                //EarthDate = selectedSolManifest.EarthDate
            };
            var result = await PhotoService.GetPhotosAsync(queryConfiguration);

            photos = result.photos;
        }


        private async void ChangePage(LoadDataArgs args)
        {
            int pageSize = 25;
            int currentPageNumber = args.Skip.HasValue
                                    ? args.Skip.Value / pageSize
                                    : 1;

            queryConfiguration.PageNumber = currentPageNumber + 1;
            var result = await PhotoService.GetPhotosAsync(queryConfiguration);

            if (result?.photos?.Count == 0)
            {
                Console.WriteLine($"No photos at page: {queryConfiguration.PageNumber}");
                return;
            }

            photos = new List<LatestPhoto>(result.photos);
            NavigationManager.NavigateTo("#top");
            StateHasChanged();
        }

        private async void DateChangedAsync(DateTime? newDate)
        {
            if (!newDate.HasValue || selectedSolManifest.EarthDate.GetValueOrDefault().DateTime.Equals(newDate.Value)) return;
                

            var newSolManifest = roverPhotoManifest.Photos.Find(o => o.EarthDate == newDate);
            if(newSolManifest is null)
                newSolManifest = roverPhotoManifest.Photos.FindLast(o => o.EarthDate < newDate);
            if(newSolManifest is null)
                newSolManifest = roverPhotoManifest.Photos.Find(o => o.EarthDate > newDate);
            

            queryConfiguration = new QueryConfiguration()
            {
                RoverName = roverPhotoManifest.Name,
                EarthDate = newSolManifest.EarthDate,
                PageNumber = 1
            };
            Console.WriteLine($"Date Changed to :{queryConfiguration.EarthDate}");
            var tempPhotos = photos;
            photos = null;
            var result = await PhotoService.GetPhotosAsync(queryConfiguration);
            if (result?.photos?.Count == 0)
                {
                    photos = tempPhotos;
                    StateHasChanged();
                    return;
                }
            selectedSolManifest = newSolManifest;

            previousSol = (selectedSolManifest.Sol > 0)
                        ? selectedSolManifest.Sol - 1
                        : null;
            nextSol = (selectedSolManifest.Sol < (roverPhotoManifest.MaxSol - 1))
                   ? selectedSolManifest.Sol + 1
                   : null;

            photos = result.photos;
            StateHasChanged();
        }
    }
}