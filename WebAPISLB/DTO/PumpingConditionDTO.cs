﻿namespace WebAPISLB.DTO
{
    public class PumpingConditionDTO
    {
        public int id { get; set; }
        public string? Pozo { get; set; }
        public string? Fecha { get; set; }
        public string? Fecha_Carga { get; set; }
        public string? Columna { get; set; }
        //public string? Operation_rate { get; set; }
        public string? Gas_rate_into_pump { get; set; }
        public string? Inlet_gas_volume_fraction { get; set; }
        public string? Total_separation_efficiency { get; set; }
        public string? Intake_pressure { get; set; }
        public string? Discharge_pressure { get; set; }
        public string? Frecuency { get; set; }
    }
}
