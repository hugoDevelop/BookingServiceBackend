namespace BookingServiceBackend.Models
{
    public class Servicio
    {
        public int ServicioId { get; set; }
        public required string Nombre { get; set; }
        public decimal Precio { get; set; }

        public ICollection<Reserva> Reservas { get; set; }
    }
}
