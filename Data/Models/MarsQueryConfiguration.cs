using System;
using System.Collections.Generic;

namespace Data.Models
{
    public record MarsPhotoBrowserConfiguration
    {
        public string NasaMarsApiUrl { get; init; }
        public string NasaMarsApiKey { get; init; }

        public List<string> Rovers {get;init;}
        
    }

    public record QueryConfiguration
    {
        public long? Sol { get; set; }

        public int PageNumber { get; set; } = 1;
        public DateTimeOffset? EarthDate { get; set; }

        public string RoverName { get; set; }

        public string Camera {get;set;}

        public long TotalPhotos {get;set;}
    }
}