using BookingServiceBackend.Models;

namespace BookingServiceBackend.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserInformationByEmail(string email);
    }
}
