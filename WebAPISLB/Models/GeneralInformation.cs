namespace WebAPISLB.Models
{
    public class GeneralInformation
    {
        public int Id { get; set; }
        public string? Pozo { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? Fecha_Carga { get; set; }
        public string? Columna1{ get; set; }
        public string? Columna2 { get; set;}
        public string? Columna3 { get; set;}
        public string? Columna4 { get; set;}
        public string? Columna5 { get; set;}
        public double? Accuracy { get; set; }
        public string? SharePointLink { get; set; }
        public int? Check { get; set; }

    }
}
