namespace Invernadero.Models
{
    public class RegistroSensor
    {
        public int IdRegistro { get; set; }
        public int IdSensor { get; set; }
        public Registro Registro { get; set; }
        public Sensor Sensor { get; set; }

    }
}
