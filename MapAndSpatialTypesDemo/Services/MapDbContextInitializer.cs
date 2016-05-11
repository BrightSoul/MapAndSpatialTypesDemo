using EntityFramework.BulkInsert.Extensions;
using MapAndSpatialTypesDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace MapAndSpatialTypesDemo.Services
{
    public class MapDbContextInitializer : DropCreateDatabaseIfModelChanges<MapDbContext>
    {
        protected override void Seed(MapDbContext context)
        {
            //Precarico i comuni italiani
            //Grazie a Loosecode per l'elenco dei comuni con coordinate geografiche
            //http://www.loosecode.com/blog/development/elenco-comuni-italiani-con-le-coordinate-geografice-con-provincie-e-regione.html

            var righe = File.ReadAllLines(HostingEnvironment.MapPath("~/App_Data/Comuni.txt"));
            var comuni = righe.Select(riga => Comune.DaRiga(riga));
            context.BulkInsert(comuni);
           
            context.SaveChanges();

        }
    }
}