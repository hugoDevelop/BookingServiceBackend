using BookingServiceBackend.Models;
using BookingServiceBackend.Repositories;

namespace BookingServiceBackend.Services
{
    public class UserService
    {
        public readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserInformationByEmail(string email) => await _userRepository.GetUserInformationByEmail(email);
    }
}
