using HairCut.Tools.Domain;
using HairCut.Tools.Repository;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace HairCut.Tools.Service
{
    public class BucketService : IBucketService
    {
        private IConfiguration _configuration { get; set; }
        private IBucketRepository _bucketRepository { get; set; }
        private IUserRepository _userRepository { get; set; }
        private IProductRepository _productRepository { get; set; }

        public BucketService(IConfiguration configuration, IBucketRepository bucketRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _configuration = configuration;
            _bucketRepository = bucketRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> CreateAsync(IFormFile file, Guid productImageId, int userId)
        {
            try
            {
                bool result = false;
                bool isValid = false;
                BucketBase bucket = new BucketBase();

                var authenticateService = new AuthenticateService(_configuration);
                isValid = this.ValidateBucket(productImageId, userId);

                if (!isValid)
                    throw new Exception("Informações de envio estão incompletas");

                var user = await _userRepository.FindByIdAsync(userId);

                if (user.Count == 0)
                    throw new Exception("O usuário não foi localizado em nossa base");

                var products = await _productRepository.FindByImageIdAsync(productImageId);

                if (products.Count == 0)
                    throw new Exception("O produto não foi localizado em nossa base");

                var product = products[0];

                if (file.Length == 0)
                    throw new Exception("Caminho de arquivo não encontrado");

                var account = new Account(
                    _configuration.GetSection("Access")["Name"],
                    _configuration.GetSection("Access")["ApiKey"],
                    _configuration.GetSection("Access")["ApiSecret"]
                );

                var cloudinary = new Cloudinary(account);

                var tempFilePath = Path.Combine(Path.GetTempPath(), file.FileName);
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(tempFilePath),
                    PublicId = productImageId.ToString(),
                    Folder = "Test"
                };

                var response = cloudinary.Upload(uploadParams);

                if (response == null)
                    throw new Exception("Não foi possível adicionar a imagem no momento");

                bucket.ImageId = productImageId;
                bucket.CreateUserId = userId;
                bucket.Path = response.SecureUri.ToString();
                bucket.CreateDate = DateTime.Now;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = true;
                    bucket.Success = true;
                }

                bucket = this.FormatAndUpdateAsync(bucket);

                await _bucketRepository.InsertAsync(bucket);

                product.ImageUrl = bucket.Url;
                await _productRepository.UpdateAsync(product);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public BucketBase FormatAndUpdateAsync(BucketBase bucket)
        {
            try
            {
                if (string.IsNullOrEmpty(bucket.Path))
                    return bucket;

                string marker = "/image/upload/";
                string path = bucket.Path;
                int markerIndex = bucket.Path.IndexOf(marker);

                if (markerIndex == -1)
                    return bucket;

                string format = "w_300,h_300,q_auto";
                string formattedUrl = path.Insert(markerIndex + marker.Length, format + "/");

                bucket.Url = formattedUrl;
                bucket.Format = true;

                return bucket;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        private bool ValidateBucket(Guid productImageId, int userId)
        {
            try
            {
                if (productImageId == Guid.Empty || productImageId == default)
                    return false;

                if (userId == 0)
                    return false;

                return true;
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
