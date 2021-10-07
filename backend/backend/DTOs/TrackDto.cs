using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs
{
    public class TrackDto
    {
        public int id;
        public string name;
        public int albumId;
        public int mediaTypeId;
        public int genreId;
        public string composer;
        public int milliseconds;
        public int bytes;
        public int unitPrice;
    }
}
