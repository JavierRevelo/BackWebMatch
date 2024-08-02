namespace WebAPISLB.DTO
{
    public class PumpingInformationDTO
    {
        public int id { get; set; }
        public string? Pozo { get; set; }
        public string? Fecha { get; set; }
        public string? Fecha_Carga { get; set; }
        public string? Columna { get; set; }
        public string? Required_power { get; set; }
        public string? Pump_efficiency { get; set; }
    }
}
