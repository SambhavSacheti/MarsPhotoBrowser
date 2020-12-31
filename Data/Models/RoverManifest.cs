using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data.Models
{
    [Table("RoverPhotoManifest")]
    public class PhotoManifest
    {
        [Key]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("landing_date")]
        public DateTimeOffset LandingDate { get; set; }

        [JsonPropertyName("launch_date")]
        public DateTimeOffset LaunchDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("max_sol")]
        public long MaxSol { get; set; }

        [JsonPropertyName("max_date")]
        public DateTimeOffset MaxDate { get; set; }

        [JsonPropertyName("total_photos")]
        public long TotalPhotos { get; set; }

        [JsonPropertyName("photos")]
        public List<Photo> Photos { get; set; }
    }



    [Table("RoverPhotoManifestDetail")]
    public class Photo
    {

       public string CamerasAsString {get;set;}

        [Key]
        public int Id { get; set; }

        [JsonPropertyName("sol")]
        public long? Sol { get; set; }

        [JsonPropertyName("earth_date")]
        public DateTimeOffset? EarthDate { get; set; }

        [JsonPropertyName("total_photos")]
        public long? TotalPhotos { get; set; }

        List<string> cameras;

        [JsonPropertyName("cameras")]
        [NotMapped]
        public List<string> Cameras { get => cameras;  set{
           cameras = value;
           CamerasAsString = string.Empty;
            foreach (var camera in value)
            {
                CamerasAsString +=camera + ',';
            }
            CamerasAsString.TrimEnd(',');
        }}
    };

    //public enum Camera { Chemcam, Fhaz, Mahli, Mardi, Mast, Navcam, Rhaz };
    [NotMapped]
    public partial class RoverManifest
    {
        [Key]
        public int Id { get; set; }
        [JsonPropertyName("photo_manifest")]
        public PhotoManifest PhotoManifest { get; set; }
    }
}