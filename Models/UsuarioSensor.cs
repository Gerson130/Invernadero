namespace Invernadero.Models
{
    public class UsuarioSensor
    {
        public int IdUsuario { get; set; }
        public int IdSensor { get; set; }
        public Usuario Usuario { get; set; }
        public Sensor Sensor { get; set; }  
    }
}
