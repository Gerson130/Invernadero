namespace Invernadero.Models
{
    public class CalendarioDeRiegoUsuario
    {
        public int IdUsuario { get; set; }
        public int IdCalendarioDeRiego { get; set; }
        public CalendarioDeRiego CalendarioDeRiego { get; set; }
        public Usuario Usuario { get; set; }
    }
}
