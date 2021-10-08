using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs
{
    public class GenreDto
    {
        public int genreId { get; set; }
        public string name { get; set; }

        override public string ToString()
        {
            return $"{genreId}, {name}";
        }
    }
}
