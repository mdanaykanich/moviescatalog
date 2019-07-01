using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FilmCatalog.Models
{
    public class dbContext: DbContext
    {
        public dbContext(string connectionString) : base(connectionString) { }
        public dbContext() : base("ConnectionString") { }

        public DbSet<Film> Films { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<FilmGenre> FilmGenres { get; set; }

    }
}