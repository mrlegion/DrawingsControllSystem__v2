using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DrawingsControllSystem.Model.Common
{
    public class Info
    {
        private readonly List<Format> formats;
        private readonly List<Marker> markers;

        public Info()
        {
            formats = File.Exists("Formats.json")
                ? JsonConvert.DeserializeObject<List<Format>>(File.ReadAllText("Formats.json"))
                : new List<Format>();

            markers = File.Exists("Markers.json")
                ? JsonConvert.DeserializeObject<List<Marker>>(File.ReadAllText("Markers.json"))
                : new List<Marker>();
        }

        public List<Format> Formats
        {
            get { return formats; }
        }

        public List<Marker> Markers
        {
            get { return markers; }
        }
    }
}