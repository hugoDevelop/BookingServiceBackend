namespace BookingServiceBackend.Models
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        public DateTime FechaReserva { get; set; }
        public int ClienteId { get; set; }
        public int ServicioId { get; set; }

        public Cliente? Cliente { get; set; }
        public Servicio? Servicio { get; set; }
    }
}
