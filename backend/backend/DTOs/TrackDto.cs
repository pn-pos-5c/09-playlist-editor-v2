using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs
{
    public class TrackDto
    {
        public int trackId { get; set; }
        public string name { get; set; }
        public int albumId { get; set; }
        public int mediaTypeId { get; set; }
        public int genreId { get; set; }
        public string composer { get; set; }
        public int milliseconds { get; set; }
        public int bytes { get; set; }
        public double unitPrice { get; set; }

        override public string ToString()
        {
            return $"{trackId}, {name}, {albumId}, {mediaTypeId}, {genreId}, {composer}, {milliseconds}, {bytes}, {unitPrice}";
        }
    }
}
