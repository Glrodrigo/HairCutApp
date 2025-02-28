using HairCut.Tools.Domain;
using HairCut.Tools.Repository;

namespace HairCut.Tools.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticateService _authenticateService;

        public UserService(IUserRepository userRepository, IAuthenticateService authenticateService) 
        {
            _userRepository = userRepository;
            _authenticateService = authenticateService;
        }

        public async Task<bool> CreateAsync(string name, string email, string password)
        {
            var user = new UserBase(name, email, password);
            bool result = false;

            var exists = await _userRepository.FindByEmailAsync(email);

            if (exists.Count > 0)
                throw new Exception("O e-mail já possui cadastro");

            var passwordContent = _authenticateService.PasswordByteAsync(user);
            user.PasswordSalt = passwordContent.PasswordSalt;
            user.PasswordHash = passwordContent.PasswordHash;

            if (user != null)
                result = await _userRepository.InsertAsync(user);

            return result;
        }

        public async Task<UserBase> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                email = email.ToLower();
                var searchUser = await _userRepository.FindByEmailAsync(email);

                if (searchUser.Count == 0)
                    throw new Exception("Usuário não localizado em nossa base");

                var user = await _authenticateService.AuthenticateAsync(email, password, searchUser);

                if (user == null)
                    throw new Exception("Usuário ou senha inválidos");

                return user;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<UserToken> LoginAsync(string email, string password)
        {
            try
            {
                var user = await this.AuthenticateUserAsync(email, password);

                if (user == null)
                    throw new Exception("Usuário ou senha inválidos");

                var userLogin = await this.UpdateLoginAsync(user);

                if (userLogin == false)
                    throw new Exception("Erro ao realizar o login de usuário");

                var token = _authenticateService.GenerateToken(user.Id, user.Email);

                UserToken userToken = new UserToken() { Token = token, Id = user.Id };

                return userToken;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateLoginAsync(UserBase user)
        {
            try
            {
                user.AccessCount = user.AccessCount + 1;
                user.SignOut = false;
                user.SessionId = Guid.NewGuid();
                user.SecurityStamp = Guid.NewGuid();
                user.AccessDate = DateTime.UtcNow;
                user.EventDate = user.AccessDate;

                var result = await _userRepository.UpdateAsync(user);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
