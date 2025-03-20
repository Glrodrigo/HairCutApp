using HairCut.Generals;
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

                var existsProduct = await _productRepository.FindAsync(product.Name, product.BrandName, product.Option);

                if (existsProduct.Count > 0)
                    throw new Exception("Produto já existente");

                product.ImageId = Guid.NewGuid();
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
            ProductTotalResults result = new ProductTotalResults();
            var pageSize = 20;

            if (pageNumber <= 0)
                throw new Exception("A página está vazia ou inválida");

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

        public async Task<ProductTotalResults> FindByCategoryAsync(int pageNumber, int categoryId)
        {
            ProductTotalResults result = new ProductTotalResults();
            var pageSize = 20;

            if (pageNumber <= 0 || categoryId <= 0)
                throw new Exception("A página ou categoria está vazia ou inválida");

            var categories = await _categoryRepository.FindByIdAsync(categoryId);

            if (categories.Count == 0)
                throw new Exception("Categoria não encontrada");

            var (products, totalPages) = await _productRepository.FindByCategoryAsync(pageNumber, pageSize, categoryId);

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

        public async Task<List<ProductResult>> FindByIdAsync(int id)
        {
            List<ProductResult> result = new List<ProductResult>();

            if (id <= 0)
                throw new Exception("A página ou categoria está vazia ou inválida");

            var products = await _productRepository.FindByIdAsync(id);

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

        public async Task<List<ProductResult>> FindByNameAsync(string name)
        {
            List<ProductResult> result = new List<ProductResult>();

            if (string.IsNullOrEmpty(name) || name == "string" || name.Length > 200)
                throw new Exception("O nome está em um formato inválido");

            name = HandleFormat.CleanName(name.ToUpper());

            var products = await _productRepository.FindByNameAsync(name);

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

        public async Task<bool> ChangeAsync(ProductParams product)
        {
            try
            {
                if (product.Id == 0 || product.UserId == 0)
                    throw new Exception("A key está vazia ou inválida");

                var admin = await AuthorizationLevel(product.UserId);

                if (!admin)
                    return false;

                var oldProducts = await _productRepository.FindByIdAsync(product.Id);

                if (oldProducts.Count == 0)
                    throw new Exception("Produto inexistente");

                var oldProduct = oldProducts[0];

                if (product.CategoryId > 0)
                {
                    var categories = await _categoryRepository.FindByIdAsync((int)product.CategoryId);

                    if (categories.Count == 0)
                        throw new Exception("Categoria não encontrada");

                    if (oldProduct.CategoryId != product.CategoryId)
                    {
                        oldProduct.CategoryId = (int)product.CategoryId;
                        oldProduct.CategoryName = categories[0].Name;
                    }
                }

                if (!string.IsNullOrEmpty(product.Name) && product.Name != "string")
                {
                    product.Name = HandleFormat.CleanName(product.Name.ToUpper());

                    if (oldProduct.Name != product.Name)
                        oldProduct.Name = product.Name;
                }

                if (!string.IsNullOrEmpty(product.BrandName) && product.BrandName != "string")
                {
                    product.BrandName = product.BrandName.ToUpper();

                    if (oldProduct.BrandName != product.BrandName)
                        oldProduct.BrandName = product.BrandName;
                }

                if (!string.IsNullOrEmpty(product.Option) && product.Option != "string")
                {
                    product.Option = HandleFormat.CleanName(product.Option.ToUpper());

                    if (oldProduct.Option != product.Option)
                        oldProduct.Option = product.Option;
                }

                if (!string.IsNullOrEmpty(product.Description) && product.Description != "string")
                {
                    if (oldProduct.Description != product.Description)
                        oldProduct.Description = product.Description;
                }

                if (product.Price > 0)
                {
                    if (oldProduct.Price != product.Price)
                        oldProduct.Price = (double)product.Price;
                }

                if (product.Total > 0)
                {
                    if (oldProduct.Total != product.Total)
                        oldProduct.Total = (int)product.Total;
                }

                oldProduct.ChangeUserId = product.UserId;
                oldProduct.EventDate = DateTime.UtcNow;

                var result = await _productRepository.UpdateAsync(oldProduct);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int userId, int id)
        {
            try
            {
                if (id == 0 || userId == 0)
                    throw new Exception("A key está vazia ou inválida");

                var admin = await AuthorizationLevel(userId);

                if (!admin)
                    return false;

                var products = await _productRepository.FindByIdAsync(id);

                if (products.Count == 0)
                    throw new Exception("A key não foi localizada em nossa base");

                var product = products[0];

                if (product.Active == false)
                    throw new Exception("Produto desativado");

                product.Active = false;
                product.ExclusionDate = DateTime.UtcNow;
                product.EventDate = product.ExclusionDate;
                product.ChangeUserId = userId;

                var result = await _productRepository.UpdateAsync(product);

                return result;
            }
            catch (Exception exception)
            {
                throw;
            }
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
