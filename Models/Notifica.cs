namespace Invernadero.Models
{
    public class Notifica
    {
        public int IdUsuario { get; set; }
        public int IdCalendarioRiego { get; set; }
        public CalendarioDeRiego CalendarioRiego;
        public Usuario Usuario;
    }
}
