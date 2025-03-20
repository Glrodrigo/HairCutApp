using Moq;
using HairCut.Tools.Service;
using HairCut.Tools.Repository;
using HairCutApp.Domain;
using HairCut.Tools.Domain;
using Microsoft.Extensions.Configuration;

namespace HairCutApp.Tests
{
    public class HairCutAppTests
    {
        private readonly IProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;

        public HairCutAppTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();

            _productService = new ProductService(
                _configurationMock.Object,
                _productRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _userRepositoryMock.Object
            );
        }

        [Fact(DisplayName = "Passar os dados válidos para a criação de um produto")]
        public async Task CreateAsync_ShouldReturnTrue_WhenProductIsValid()
        {
            // Arrange
            var product = new ProductDomain
            {
                UserId = 1,
                Name = "Produto teste",
                BrandName = "ABC",
                Option = "Amostra",
                Description = "Descrição do produto",
                Price = 8.99,
                CategoryId = 1,
                Total = 3
            };

            ProductBase productBase = new ProductBase(product.Name, product.BrandName, product.Option, product.Description, product.Price, product.CategoryId, product.Total);

            _productRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<ProductBase>()))
                .ReturnsAsync(true);

            _productRepositoryMock
                .Setup(repo => repo.FindAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<ProductBase>());

            _categoryRepositoryMock
                .Setup(repo => repo.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<CategoryBase> { new CategoryBase("Teste") { Id = 1, Name = "Categoria Teste" } });

            _userRepositoryMock
                .Setup(repo => repo.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<UserBase> { new UserBase("Usuário Teste", "teste@gmail.com", "Senha@123") { ProfileId = Guid.Parse("0017dae5-cab2-457f-8d03-96092cc9fd94") } });

            _configurationMock
                .Setup(config => config.GetSection("Access")["SecretKey"])
                .Returns("0017dae5-cab2-457f-8d03-96092cc9fd94");

            // Act
            var result = await _productService.CreateAsync(productBase, product.UserId);

            // Assert
            Assert.True(result);
            _productRepositoryMock.Verify(repo => repo.InsertAsync(It.IsAny<ProductBase>()), Times.Once);
        }


        [Fact(DisplayName = "Listar todos os produtos")]
        public async Task GetAsync_ShouldReturnListOfProducts()
        {
            // Arrange
            var products = new List<ProductBase>
            {
                new ProductBase("Primeiro Produto", "Abc", "Amostra", "Amostra de Produto 1", 5.99, 1, 5){ Id = 1 },
                new ProductBase("Segundo Produto", "Dfg", "Amostra", "Amostra de Produto 2", 7.99, 1, 7){ Id = 2 },
                new ProductBase("Terceiro Produto", "Hij", "Amostra", "Amostra de Produto 3", 9.99, 1, 10){ Id = 3 }
            };

            _productRepositoryMock.Setup(repo => repo.GetAsync())
                .ReturnsAsync(products);

            // Act
            var result = await _productService.GetAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("PRIMEIRO PRODUTO", result[0].Name);
            Assert.Equal("SEGUNDO PRODUTO", result[1].Name);
            Assert.Equal("TERCEIRO PRODUTO", result[2].Name);
            _productRepositoryMock.Verify(repo => repo.GetAsync(), Times.Once);
        }

        [Fact(DisplayName = "Deletar um produto existente")]
        public async Task DeleteProductAsync_ShouldReturnTrue_WhenProductExists()
        {
            // Arrange
            int userId = 1;
            ProductBase product = new ProductBase("Primeiro Produto", "Abc", "Amostra", "Amostra de Produto 1", 5.99, 1, 5) { Id = 1, Active = true };

            _productRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<ProductBase>()))
                .ReturnsAsync(true);

            _userRepositoryMock
                .Setup(repo => repo.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<UserBase> { new UserBase("Usuário Teste", "teste@gmail.com", "Senha@123") { ProfileId = Guid.Parse("0017dae5-cab2-457f-8d03-96092cc9fd94") } });

            _configurationMock
                .Setup(config => config.GetSection("Access")["SecretKey"])
                .Returns("0017dae5-cab2-457f-8d03-96092cc9fd94");

            _productRepositoryMock
                .Setup(repo => repo.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<ProductBase> { new ProductBase("Primeiro Produto", "Abc", "Amostra", "Amostra de Produto 1", 5.99, 1, 5) { Id = 1, Active = true } });

            // Act
            var result = await _productService.DeleteAsync(userId, product.Id);

            // Assert
            Assert.True(result);
            _productRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<ProductBase>()), Times.Once);
        }
    }
}