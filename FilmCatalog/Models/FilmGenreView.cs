using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmCatalog.Models
{
    public class FilmGenreView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string PosterPath { get; set; }
        public List<string> Genres { get; set; }
        public string PosterBGPath { get; set; }
        public string Country { get; set; }
        public FilmGenreView()  
        {
            Genres = new List<string>();
        }
    }
}