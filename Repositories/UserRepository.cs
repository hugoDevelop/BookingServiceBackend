using BookingServiceBackend.Data;
using BookingServiceBackend.Exceptions;
using BookingServiceBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingServiceBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookingContext _context;

        public UserRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserInformationByEmail(string email)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Company).FirstOrDefaultAsync(u => u.Email == email);
                return user ?? throw new NotFoundException("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al obtener la información del usuario: " + ex.Message);
            }
        }
    }
}

