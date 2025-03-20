using HairCut.Tools.Domain;
using Microsoft.EntityFrameworkCore;

namespace HairCut.Tools.Repository
{
    public  class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(ProductBase product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar salvar no banco de dados", ex);
            }
        }

        public async Task<List<ProductBase>> FindAsync(string name, string brandName, string option)
        {
            try
            {
                var products = await _context.Products
                    .Where(t => t.Name == name && t.BrandName == brandName && t.Option == option)
                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter o produto do banco de dados", ex);
            }
        }

        public async Task<List<ProductBase>> FindByNameAsync(string name)
        {
            try
            {
                var products = await _context.Products
                    .Where(t => t.Name == name)
                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter o produto do banco de dados", ex);
            }
        }

        public async Task<List<ProductBase>> FindByIdAsync(int id)
        {
            try
            {
                var products = await _context.Products
                    .Where(t => t.Id == id)
                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter o produto do banco de dados", ex);
            }
        }

        public async Task<List<ProductBase>> FindByImageIdAsync(Guid imageId)
        {
            try
            {
                var products = await _context.Products
                    .Where(t => t.ImageId == imageId)
                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter o produto do banco de dados", ex);
            }
        }

        public async Task<List<ProductBase>> GetAsync()
        {
            try
            {
                return await _context.Products.ToListAsync() ?? new List<ProductBase>();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter os produtos do banco de dados", ex);
            }
        }

        public async Task<(List<ProductBase>, int TotalPages)> GetByPaginationAsync(int pageNumber, int pageSize)
        {
            try
            {
                int totalRecords = await _context.Products
                    .Where(c => c.Active)
                    .CountAsync();

                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                var products = await _context.Products
                                     .Where(p => p.Active == true)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

                return (products, totalPages);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter filtro do banco de dados", ex);
            }
        }

        public async Task<(List<ProductBase>, int TotalPages)> FindByCategoryAsync(int pageNumber, int pageSize, int categoryId)
        {
            try
            {
                int totalRecords = await _context.Products
                    .Where(p => p.Active && p.CategoryId == categoryId)
                    .CountAsync();

                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                var products = await _context.Products
                                     .Where(p => p.Active == true && p.CategoryId == categoryId)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

                return (products, totalPages);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter filtro do banco de dados", ex);
            }
        }

        public async Task<bool> UpdateAsync(ProductBase product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar atualizar produto", ex);
            }
        }
    }
}
