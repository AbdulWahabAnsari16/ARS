﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARS.Models
{
    public class FlightRoutes
    {
        [Key]
        public int RouteId { get; set; }

        public int OriginAirportId { get; set; }
        [ForeignKey("OriginAirportId")]
        public Airport OriginAirport { get; set; }

        public int DestinationAirportId { get; set; }
        [ForeignKey("DestinationAirportId")]
        public Airport DestinationAirport { get; set; }

        public double Distance { get; set; }
    }
}