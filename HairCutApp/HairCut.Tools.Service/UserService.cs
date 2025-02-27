using HairCut.Tools.Domain;
using HairCut.Tools.Repository;

namespace HairCut.Tools.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(string name, string email, string password)
        {
            var user = new UserBase(name, email, password);
            bool result = false;

            if (user != null)
                result = await _userRepository.InsertAsync(user);

            return result;
        }
    }
}
