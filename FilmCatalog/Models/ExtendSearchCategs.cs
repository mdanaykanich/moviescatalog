using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmCatalog.Models
{
    public class ExtendSearchCategs
    {
        public List<string> Genres { get; set; }
        public List<string> Countries { get; set; }
        public List<int> Years { get; set; }
        public List<FilmGenreView> Films { get; set; }

        public ExtendSearchCategs()
        {
            Genres = new List<string>();
            Countries = new List<string>();
            Years = new List<int>();
            Films = new List<FilmGenreView>();
        }
    }
}