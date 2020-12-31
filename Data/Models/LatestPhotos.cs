using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class Camera
    {
        public int id { get; set; }
        public string name { get; set; }
        public int rover_id { get; set; }
        public string full_name { get; set; }
    }

    public class Rover
    {
        public int id { get; set; }
        public string name { get; set; }
        public string landing_date { get; set; }
        public string launch_date { get; set; }
        public string status { get; set; }
    }

    public enum Rovers {Curiosity, Opportunity, Spirit};

    
    public class LatestPhoto
    {
        //public int id { get; set; }
        public int sol { get; set; }
        public Camera camera { get; set; }
        public string img_src { get; set; }
        public string earth_date { get; set; }
        public Rover rover { get; set; }
    }
    public class LatestPhotos
    {
        public List<LatestPhoto> photos { get; set; }
        public List<Camera> Cameras { get; init; }

        public List<Rover> Rovers { get; init; }

        public LatestPhotos()
        {
            Cameras = buildRoverCamerasConfig();
            Rovers = buildRoversConfig();
        }

        private List<Camera> buildRoverCamerasConfig() =>
         new (){
                new (){
                    id=20,
                    name="FHAZ",
                    rover_id=5,
                    full_name="Front Hazard Avoidance Camera"
                },
                new (){
                    id=21,
                    name="RHAZ",
                    rover_id=5,
                    full_name="Rear Hazard Avoidance Camera"
                },
                new (){
                    id=21,
                    name="MAST",
                    rover_id=5,
                    full_name="Mast Camera"
                },
                 new (){
                    id=-1,
                    name="CHEMCAM",
                    rover_id=-1,
                    full_name="Chemistry and Camera Complex"
                },
                new (){
                    id=-1,
                    name="MAHLI",
                    rover_id=-1,
                    full_name="Mars Hand Lens Imager"
                },
                new (){
                    id=-1,
                    name="MARDI",
                    rover_id=-1,
                    full_name="Mars Descent Imager"
                },
                  new (){
                    id=-1,
                    name="NAVCAM",
                    rover_id=-1,
                    full_name="Navigation Camera"
                },
                new (){
                    id=-1,
                    name="PANCAM",
                    rover_id=-1,
                    full_name="Panoramic Camera"
                },
                new (){
                    id=-1,
                    name="MINITES",
                    rover_id=-1,
                    full_name="Miniature Thermal Emission Spectrometer"
                },

            };

        private List<Rover> buildRoversConfig() =>
        new (){
            new(){
            id = 1,
            
            }
        };

    }

}