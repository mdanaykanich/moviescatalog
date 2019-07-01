using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmCatalog.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public int FilmId { get; set; }
    }
}