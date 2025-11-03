namespace Invernadero.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public List<Usuario> Usuario { get; set; } = new List<Usuario>();
        public List<UsuarioSensor> UsuarioSensor {  get; set; } = new List<UsuarioSensor>();
        public List<RegistroSensor> RegistroSensor { get; set; } = new List<RegistroSensor>();
    }
}
