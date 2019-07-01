using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmCatalog.Models
{
    public class FilmGenreViewList
    {
        public List<FilmGenreView> fgvl { get; set; }

        public FilmGenreViewList()
        {
            fgvl = new List<FilmGenreView>();
        }
    }
}