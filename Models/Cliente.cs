namespace BookingServiceBackend.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public required string Nombre { get; set; }
        public required string Email { get; set; }
        public string? Telefono { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = [];
    }
}
