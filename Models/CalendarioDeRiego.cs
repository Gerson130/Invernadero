using System.ComponentModel.DataAnnotations;

namespace Invernadero.Models
{
    public class CalendarioDeRiego
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Tipo { get; set; }
        [Display(Name ="Nivel de agua (ml)")]
        public int NivelAgua { get; set; } 
        public List<Notifica> Notificacion {  get; set; } = new List<Notifica>();
        public List<CalendarioDeRiegoUsuario> CalendarioRiegoUsuario { get; set; } = new List<CalendarioDeRiegoUsuario>();
        
    }
}
