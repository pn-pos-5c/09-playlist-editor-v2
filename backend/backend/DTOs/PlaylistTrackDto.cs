using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs
{
    public class PlaylistTrackDto
    {
        public int playlistId { get; set; }
        public int trackId { get; set; }

        override public string ToString()
        {
            return $"{playlistId}, {trackId}";
        }
    }
}
