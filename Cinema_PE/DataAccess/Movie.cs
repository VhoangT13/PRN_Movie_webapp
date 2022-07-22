using System;
using System.Collections.Generic;

#nullable disable

namespace Cinema_PE.DataAccess
{
    public partial class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int GenreId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
