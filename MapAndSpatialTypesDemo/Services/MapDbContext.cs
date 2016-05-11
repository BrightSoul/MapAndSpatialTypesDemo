using MapAndSpatialTypesDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MapAndSpatialTypesDemo.Services
{
    public class MapDbContext : DbContext
    {
        public DbSet<Comune> Comuni { get; set; }
    }
}