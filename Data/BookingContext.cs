﻿using BookingServiceBackend.Models;
using BookingServiceBackend.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookingServiceBackend.Data
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbInitializer dbInitializer = new(modelBuilder);
            dbInitializer.Seed();
        }
    }
}
