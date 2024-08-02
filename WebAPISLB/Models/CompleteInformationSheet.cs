 namespace WebAPISLB.Models
{
    public class CompleteInformationSheet
    {
        public int Id { get; set; }
        public string? Pozo { get; set; }
        public DateOnly? Fecha { get; set; }
        public DateOnly? Fecha_Carga { get; set; }
        public string? Columna { get; set; }
        public double? Wellhead_Pressure { get; set; }
        public double? Oil_Gravity { get; set; }
        public double? Water_Cut { get; set; }
        public double? GOR { get; set; }
        public double? GLR { get; set; }
        public double? PI { get; set; }
        public double? Static_Bottombole_Pressure { get; set; }
        public double? Gas_Rate_Into_Pump { get; set; }
        public double? Inlet_Gas_Volume_Fraction { get; set; }
        public double? Total_Separation_Efficiency { get; set; }
        public double? Intake_Pressure { get; set; }
        public double? Discharge_Pressure { get; set; }
        public double? Frecuency { get; set; }
        public double? Required_Power { get; set; }
        public double? Pump_Efficiency { get; set; }
        public double? Motor_Horse_Power { get; set; }
        public double? Motor_Amperage { get; set; }
        public double? Motor_Voltage { get; set; }
        public double? Load_Factor { get; set; }
        public double? Efficiency { get; set; }
        public double? Total_Winding_Temperature { get; set; }
        public double? Surface_Voltage { get; set; }
        public double? Required_KVA { get; set; }
        public string? Methoth { get; set; }
    }
}
