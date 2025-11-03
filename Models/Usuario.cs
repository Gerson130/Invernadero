namespace Invernadero.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contrasenia { get; set; }
        public List<Notifica> Notificacion { get; set; } = new List<Notifica>();
        public List<UsuarioSensor> UsuarioSensor { get; set; } = new List<UsuarioSensor>();
        public List<CalendarioDeRiegoUsuario> CalendarioRiegoUsuario { get; set; } = new List<CalendarioDeRiegoUsuario>();
    }
}
