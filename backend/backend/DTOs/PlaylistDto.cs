using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs
{
    public class PlaylistDto
    {
        public int playlistId { get; set; }
        public string name { get; set; }

        override public string ToString()
        {
            return $"{playlistId}, {name}";
        }
    }
}
