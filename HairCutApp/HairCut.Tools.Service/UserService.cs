using HairCut.Tools.Domain;
using HairCut.Tools.Repository;
using Microsoft.Extensions.Configuration;

namespace HairCut.Tools.Service
{
    public class UserService : IUserService
    {
        private IConfiguration _configuration { get; set; }
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticateService _authenticateService;

        public UserService(IConfiguration configuration, IUserRepository userRepository, IAuthenticateService authenticateService) 
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _authenticateService = authenticateService;
        }

        public async Task<bool> CreateAsync(string name, string email, string password)
        {
            try
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
            catch (Exception exception)
            {
                throw;
            }
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
                if (string.IsNullOrEmpty(email) || !HairCut.Generals.StringFormat.isEmail(email))
                    throw new Exception("O e-mail está em um formato inválido");

                if (string.IsNullOrEmpty(password))
                    throw new Exception("A senha está vazia ou inválida");

                email = email.ToLower();

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

        public async Task<bool> UserAuthorization(int userId)
        {
            try
            {
                var user = await _userRepository.FindByIdAsync(userId);

                if (user.Count == 0)
                    throw new Exception("A key não foi localizada em nossa base");

                Guid privilege = Guid.Parse(_configuration.GetSection("Access")["SecretKey"]);

                if (user[0].Active == true && user[0].ProfileId == privilege)
                    return true;

                return false;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<bool> ValidateUserAuthorization(int userId)
        {
            try
            {
                var authorization = await this.UserAuthorization(userId);

                if (!authorization)
                    throw new Exception("Acesso não autorizado");

                return authorization;
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
