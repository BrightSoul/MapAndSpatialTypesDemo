using MapAndSpatialTypesDemo.Models;
using MapAndSpatialTypesDemo.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MapAndSpatialTypesDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Intersezione([ModelBinder(typeof(DbGeographyModelBinder))] DbGeography forma)
        {
            using (var context = new MapDbContext())
            {
                var comuni = await context.Comuni
                    .Where(comune => comune.Posizione.Intersects(forma))
                    .OrderByDescending(comune => (int) comune.Ruolo) //Mettiamo in primo piano i capoluoghi
                    .Select(comune => new {
                        comune.Nome,
                        comune.Provincia,
                        comune.Ruolo,
                        Lat = comune.Posizione.Latitude,
                        Lon = comune.Posizione.Longitude })
                    .Take(100)
                    .ToListAsync();
                return Json(new { Comuni = comuni });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Distanza([ModelBinder(typeof(DbGeographyModelBinder))] DbGeography forma, double distanzaInMetri)
        {
            using (var context = new MapDbContext())
            {
                var comuni = await context.Comuni
                    .Where(comune => comune.Posizione.Distance(forma) < distanzaInMetri)
                    .OrderByDescending(comune => (int)comune.Ruolo) //Mettiamo in primo piano i capoluoghi
                    .Select(comune => new {
                        comune.Nome,
                        comune.Provincia,
                        comune.Ruolo,
                        Lat = comune.Posizione.Latitude,
                        Lon = comune.Posizione.Longitude
                    })
                    .Take(100)
                    .ToListAsync();
                return Json(new { Comuni = comuni });
            }
        }


    }
}