using HairCut.Generals;
using HairCut.Tools.Domain;
using HairCut.Tools.Repository;
using Microsoft.Extensions.Configuration;

namespace HairCut.Tools.Service
{
    public class AccessService : IAccessService
    {
        private IConfiguration _configuration { get; set; }
        private readonly IAccessRepository _accessRepository;
        private IUserRepository _userRepository { get; set; }

        public AccessService(IConfiguration configuration, IAccessRepository accessRepository, IUserRepository userRepository)
        {
            _configuration = configuration;
            _accessRepository = accessRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(AccessBase access, int userId)
        {
            try
            {
                var admin = await AuthorizationLevel(userId);

                if (!admin)
                    return false;

                access = this.HandleAccess(access);

                var existsProfile = await _accessRepository.FindByNameAsync(access.AccountName, access.ProfileName);

                if (existsProfile.Count > 0)
                    throw new Exception("Perfil de usuário existente");

                access.ProfileId = Guid.NewGuid();
                access.AccountId = Guid.NewGuid();
                access.CreateDate = DateTime.UtcNow;
                access.Active = true;
                access.CreateUserId = userId;

                return await _accessRepository.InsertAsync(access);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<bool> ChangeAsync(AccessBase access, int userId, Guid profileId)
        {
            try
            {
                var user = await _userRepository.FindByIdAsync(userId);

                if (user.Count == 0)
                    throw new Exception("A key não foi localizada em nossa base");

                if (profileId == Guid.Empty || profileId == default)
                    throw new Exception("A key está vazia ou inválida");

                var oldAccesses = await _accessRepository.FindByProfileIdAsync(profileId);

                if (oldAccesses.Count == 0)
                    throw new Exception("Perfil de usuário inexistente");

                var oldAccess = oldAccesses[0];

                access = this.HandleAccess(access);

                if (oldAccess.AccountName != access.AccountName)
                    oldAccess.AccountName = access.AccountName;

                if (oldAccess.ProfileName != access.ProfileName)
                    oldAccess.ProfileName = access.ProfileName;

                if (oldAccess.LevelCode != access.LevelCode)
                    oldAccess.LevelCode = access.LevelCode;

                if (oldAccess.RuleCode != access.RuleCode)
                    oldAccess.RuleCode = access.RuleCode;

                if (oldAccess.RuleName != access.RuleName)
                    oldAccess.RuleName = access.RuleName;

                if (oldAccess.Color != access.Color)
                    oldAccess.Color = access.Color;

                oldAccess.ChangeUserId = userId;
                oldAccess.EventDate = DateTime.UtcNow;

                var result = await _accessRepository.UpdateAsync(oldAccess);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }


        public async Task<bool> ChangeUserAccessAsync(int id, int userId, Guid profileId)
        {
            try
            {
                var admin = await AuthorizationLevel(id);

                if (!admin)
                    return false;

                if (profileId == Guid.Empty || profileId == default)
                    throw new Exception("A key está vazia ou inválida");

                var accesses = await _accessRepository.FindByProfileIdAsync(profileId);

                if (accesses.Count == 0)
                    throw new Exception("Perfil de usuário inexistente");

                var users = await _userRepository.FindByIdAsync(userId);

                if (users.Count == 0)
                    throw new Exception("Perfil de usuário inexistente");

                var user = users[0];

                user.EventDate = DateTime.UtcNow;
                user.ProfileId = profileId;
                user.ChangeUserId = id;

                var result = await _userRepository.UpdateAsync(user);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int userId, Guid profileId)
        {
            try
            {
                var admin = await AuthorizationLevel(userId);

                if (!admin)
                    return false;

                if (profileId == Guid.Empty || profileId == default)
                    throw new Exception("A key está vazia ou inválida");

                var accesses = await _accessRepository.FindByProfileIdAsync(profileId);

                if (accesses.Count == 0)
                    throw new Exception("A key não foi localizada em nossa base");

                var access = accesses[0];

                if (access.Active == false)
                    throw new Exception("Perfil desativado");

                access.Active = false;
                access.ExclusionDate = DateTime.UtcNow;
                access.EventDate = access.ExclusionDate;
                access.ChangeUserId = userId;

                var result = await _accessRepository.UpdateAsync(access);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public AccessBase HandleAccess(AccessBase access)
        {
            try
            {
                access.AccountName = HandleFormat.CleanName(access.AccountName);
                access.ProfileName = HandleFormat.CleanName(access.ProfileName);

                if (access.AccountName.Length < 3 || access.ProfileName.Length < 3)
                    throw new Exception("Nome de conta ou perfil inválido");

                access.AccountName = access.AccountName.ToUpper();
                access.ProfileName = access.ProfileName.ToUpper();

                if (!string.IsNullOrEmpty(access.Color) && !StringFormat.IsHexColor(access.Color))
                    throw new Exception("Código de cor inválido");

                access = HandleEnumRules(access);

                return access;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public AccessBase HandleEnumRules(AccessBase access)
        {
            if (access.LevelCode == 1)
            {
                access.LevelCode = 1;
                access.RuleCode = 1;
                access.RuleName = "View all options";
                access.Color = "#2a4e7d";
            }
            else
            {
                access.LevelCode = 0;
                access.RuleCode = 0;
                access.RuleName = "View only selected options";
                access.Color = "#999185";
            }

            return access;
        }

        public async Task<bool> AuthorizationLevel(int userId)
        {
            try
            {
                Guid privilege = Guid.Parse(_configuration.GetSection("Access")["SecretKey"]);
                var user = await _userRepository.FindByIdAsync(userId);

                if (user.Count == 0)
                    throw new Exception("A key não foi localizada em nossa base");

                if (user[0].Active != true || user[0].ProfileId != privilege)
                    return false;

                return true;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public List<string> HandleFeatureCodes(List<string>? featureCodes)
        {
            try
            {
                string featureCode;
                List<string> codes = new List<string>();

                if (featureCodes != null || featureCodes.Count > 0)
                {
                    foreach (var code in featureCodes)
                    {
                        featureCode = HandleFormat.StringCodeFormat(code);

                        if (featureCode.Length != 5)
                            return null;

                        codes.Add(featureCode);
                    }
                }

                return codes;
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
