namespace WebAPISLB.DTO
{
    public class HeatRiseDTO
    {
        public int id { get; set; }
        public string? Pozo { get; set; }
        public string? Fecha { get; set; }
        public string? Fecha_Carga { get; set; }
        public string? Columna { get; set; }
        public string? Total_winding_temperature { get; set; }
    }
}
