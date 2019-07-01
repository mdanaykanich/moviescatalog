using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilmCatalog.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FilmCatalog.Controllers
{
    public class HomeController : Controller
    {
        dbContext dbc = new dbContext();

        public async Task<FilmGenreView> GetFilmById(int id)
        {
            FilmGenreView fgv = new FilmGenreView();

            await Task.Run(() =>
            {
                fgv.Id = id;
                fgv.Title = dbc.Films.Where(f => f.Id == id).First().Title;
                fgv.Description = dbc.Films.Where(f => f.Id == id).First().Description;
                fgv.Year = dbc.Films.Where(f => f.Id == id).First().Year;
                fgv.PosterPath = $"/Content/Posters/{dbc.Films.Where(f => f.Id == id).First().PosterPath}";
                fgv.PosterBGPath = $"/Content/Posters/{dbc.Films.Where(f => f.Id == id).First().PosterBGPath}";
                fgv.Country = dbc.Films.Where(f => f.Id == id).First().Country;
                var g = from fg in dbc.FilmGenres
                        from film in dbc.Films
                        from genre in dbc.Genres
                        where fg.FilmId == id
                        where fg.GenreId == genre.Id
                        where fg.FilmId == film.Id
                        select genre.Name;
                foreach (var genre in g)
                {
                    fgv.Genres.Add(genre);
                }
            });
            return fgv;

        }

        public async Task<ActionResult> Filmsearch(string partialTitle)
        {
            List<FilmGenreView> fl = new List<FilmGenreView>();
            List<int> idFilmsArray = new List<int>();
            idFilmsArray = dbc.Films.Where(f => f.Title.Contains(partialTitle)).Select(film => film.Id).ToList();
            foreach (int id in idFilmsArray)
                fl.Add(await GetFilmById(id));
            return View("Films", fl);
        }

        public async Task<ActionResult> Films(int year = 0, string genre = "all", string country = "all")
        {
            List<int> idFilmsByGenre = new List<int>();
            List<FilmGenreView> fl = new List<FilmGenreView>();
            List<int> idFilmsArray = new List<int>();
            int GenreId = -1;

            if (genre == "all")
            {
                idFilmsByGenre = dbc.Films.Select(f => f.Id).ToList();
            }
            else {
                GenreId = dbc.Genres.Where(g => g.Name == genre).Select(gen => gen.Id).First();
                idFilmsByGenre = dbc.FilmGenres.Where(fg => fg.GenreId == GenreId).Select(filmgenre => filmgenre.FilmId).ToList();
            }

            if (country == "all")
                country = "";

            if (year != 0)
            {
                idFilmsArray = dbc.Films.Where(f => f.Year == year && f.Country.Contains(country) && idFilmsByGenre.Contains(f.Id)).Select(film => film.Id).ToList();
                foreach (int id in idFilmsArray)
                    fl.Add(await GetFilmById(id));
            }
            else
            {
                idFilmsArray = dbc.Films.Where(f => f.Country.Contains(country) && idFilmsByGenre.Contains(f.Id)).Select(film => film.Id).ToList();
                foreach (int id in idFilmsArray)
                    fl.Add(await GetFilmById(id));
            }
            return View(fl);
        }

        public async Task<ActionResult> Index()
        {

            FilmGenreViewList fl = new FilmGenreViewList();
            var filmsId = dbc.Films.Select(f => f.Id).ToList();

            foreach (var fId in filmsId)
            {
                fl.fgvl.Add(await GetFilmById(fId));
            }
            return View(fl);
        }

        public async Task<ActionResult> ExtendedSearch(string country = "", string genre = "", int year = 0)
        {
            ExtendSearchCategs esc = new ExtendSearchCategs();
            foreach (var f in dbc.Films)
            {
                if (f.Country.Contains(", "))
                    esc.Countries.AddRange(f.Country.Split(new string[] { ", " }, StringSplitOptions.None).ToList());
                else
                    esc.Countries.Add(f.Country);
            }
            esc.Countries = esc.Countries.Distinct().ToList();
            esc.Genres = dbc.Genres.Select(g => g.Name).ToList();
            esc.Years = dbc.Films.Select(f => f.Year).Distinct().ToList();

            var filmsId = dbc.Films.Select(f => f.Id).ToList();
            foreach (var id in filmsId)
                esc.Films.Add(await GetFilmById(id));
            return View(esc);
        }

        [HttpGet]
        public ActionResult AddFilm()
        {
            ViewBag.genres = dbc.Genres.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddFilm(FilmGenreView fg)
        {
            List<string> genres = new List<string>();
            foreach(var gen in fg.Genres)
            {
                if (gen != "false")
                    genres.Add(gen);
            }

            Film film = new Film();
            film.Title = fg.Title;
            film.Description = fg.Description;
            film.Year = fg.Year;
            film.PosterPath = fg.PosterPath;
            film.PosterBGPath = fg.PosterBGPath;
            film.Country = fg.Country;

            dbc.Films.Add(film);
            dbc.SaveChanges();

            int filmId = dbc.Films.Where(ff => ff.Title == fg.Title).Select(f => f.Id).First(); 

            foreach(var genre in genres)
            {
                dbc.FilmGenres.Add(new FilmGenre() { FilmId = filmId, GenreId = dbc.Genres.Where(gg => gg.Name == genre).Select(g => g.Id).First() });
            }

            dbc.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult GetFilmId(string filmTitle)
        {
            int filmId;
            try
            {
                filmId = dbc.Films.Where(f => f.Title == filmTitle).First().Id;
            }
            catch (Exception ex)
            {
                filmId = -1;
            }
            return Json(filmId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Coincidences(string val)
        {
            List<string> res = new List<string>();
            res = dbc.Films.Where(f => f.Title.StartsWith(val)).Select(f => f.Title).ToList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Details(int id)
        {
            FilmGenreView film = await GetFilmById(id);

            return View(film);
        }
    }
}