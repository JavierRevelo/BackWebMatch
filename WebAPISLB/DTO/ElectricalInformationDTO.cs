namespace WebAPISLB.DTO
{
    public class ElectricalInformationDTO
    {
        public int id { get; set; }
        public string? Pozo { get; set; }
        public string? Fecha { get; set; }
        public string? Fecha_Carga { get; set; }
        public string? Columna { get; set; }
        public string? Surface_voltage { get; set; }
        public string? Required_kVA { get; set; }
    }
}
