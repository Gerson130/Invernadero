namespace Invernadero.Models
{
    public class Registro
    {
        public int Id { get; set; }
        public decimal Temperatura { get; set; }
        public decimal Humedad { get; set; }
        public DateTime Hora { get; set; }
        public List<RegistroSensor> RegistroSensor { get; set; } = new List<RegistroSensor>();
    }
}
