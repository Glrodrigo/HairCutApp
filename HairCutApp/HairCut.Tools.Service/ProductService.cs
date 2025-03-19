using HairCut.Tools.Domain;
using HairCut.Tools.Repository;
using Microsoft.Extensions.Configuration;

namespace HairCut.Tools.Service
{
    public class ProductService : IProductService
    {
        private IConfiguration _configuration { get; set; }
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private IUserRepository _userRepository { get; set; }

        public ProductService(IConfiguration configuration, IProductRepository productRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _configuration = configuration;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(ProductBase product, int userId)
        {
            try
            {
                var admin = await AuthorizationLevel(userId);

                if (!admin)
                    return false;

                var categories = await _categoryRepository.FindByIdAsync(product.CategoryId);

                if (categories.Count == 0)
                    throw new Exception("Categoria não encontrada");

                product.CategoryName = categories[0].Name;

                var existsProduct = await _productRepository.FindByNameAsync(product.Name, product.BrandName, product.Option);

                if (existsProduct.Count > 0)
                    throw new Exception("Produto já existente");

                product.CreateDate = DateTime.UtcNow;
                product.Active = true;
                product.CreateUserId = userId;

                return await _productRepository.InsertAsync(product);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<List<ProductResult>> GetAsync()
        {
            try
            {
                var products = await _productRepository.GetAsync();
                List<ProductResult> result = new List<ProductResult>();

                foreach (var prod in products)
                {
                    ProductResult product = new ProductResult()
                    {
                        Name = prod.Name,
                        BrandName = prod.BrandName,
                        Option = prod.Option,
                        Price = prod.Price,
                        Image = prod.ImageUrl,
                        CategoryName = prod.CategoryName,
                        Total = prod.Total
                    };

                    result.Add(product);
                }

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<ProductTotalResults> GetByPageAsync(int pageNumber)
        {
            if (pageNumber <= 0)
                throw new Exception("A página está vazia ou inválida");

            ProductTotalResults result = new ProductTotalResults();
            var pageSize = 20;

            var (products, totalPages) = await _productRepository.GetByPaginationAsync(pageNumber, pageSize);

            foreach (var prod in products)
            {
                ProductResult product = new ProductResult()
                {
                    Name = prod.Name,
                    BrandName = prod.BrandName,
                    Option = prod.Option,
                    Price = prod.Price,
                    Image = prod.ImageUrl,
                    CategoryName = prod.CategoryName,
                    Total = prod.Total
                };

                result.Products.Add(product);
            }

            if (products.Count > 0)
                result.TotalPages = totalPages;

            return result;
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
    }
}
