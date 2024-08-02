namespace WebAPISLB.DTO
{
    public class MotorInformationDTO
    {
        public int id { get; set; }
        public string? Pozo { get; set; }
        public string? Fecha { get; set; }
        public string? Fecha_Carga { get; set; }
        public string? Columna { get; set; }
        public string? Motor_horse_power { get; set; }
        public string? Motor_amperage { get; set; }
        public string? Motor_voltage { get; set; }
        public string? Load_factor { get; set; }
        public string? Efficiency { get; set; }
    }
}
