using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmCatalog.Models
{
    public class FilmGenre
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int GenreId { get; set; }
    }
}