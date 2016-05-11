using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MapAndSpatialTypesDemo.Models
{
    [Table("Comuni")]
    public class Comune
    {
        [Key, StringLength(6, MinimumLength = 6)]
        public string CodiceIstat { get; set; }
        [Required, StringLength(255)]
        public string Nome { get; set; }
        [Required, StringLength(2, MinimumLength = 2)]
        public string Provincia { get; set; }
        public RuoloAmministrativo Ruolo { get; set; }
        public DbGeography Posizione { get; set; }

        public static Comune DaRiga(string riga)
        {
            var campi = riga.Split('\t');
            return new Comune
            {
                CodiceIstat = campi[0],
                Nome = campi[1],
                Provincia = campi[2],
                Ruolo = (RuoloAmministrativo) Enum.Parse(typeof(RuoloAmministrativo), campi[3]),
                Posizione = DbGeography.FromText(string.Format("POINT({1} {0})",
                campi[4], //latitudine e longitudine devono essere formattati col punto come separatore dei decimali
                campi[5],
                4326)) //Sistema di coordinate EPSG:4326, quello basato su latitudine e longitudine che usiamo comunemente
            };
        }
    }

}