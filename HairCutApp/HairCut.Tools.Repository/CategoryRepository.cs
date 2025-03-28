﻿using HairCut.Tools.Domain;
using Microsoft.EntityFrameworkCore;

namespace HairCut.Tools.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(CategoryBase category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar salvar no banco de dados", ex);
            }
        }

        public async Task<List<CategoryBase>> FindByNameAsync(string name)
        {
            try
            {
                var categories = await _context.Categories
                    .AsNoTracking()
                    .Where(t => t.Name == name)
                    .ToListAsync();

                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter a categoria do banco de dados", ex);
            }
        }

        public async Task<List<CategoryBase>> FindByIdAsync(int id)
        {
            try
            {
                var accesses = await _context.Categories
                    .Where(t => t.Id == id)
                    .ToListAsync();

                return accesses;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter a categoria do banco de dados", ex);
            }
        }

        public async Task<List<CategoryBase>> GetAsync()
        {
            try
            {
                return await _context.Categories.AsNoTracking().ToListAsync() ?? new List<CategoryBase>();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter as categorias do banco de dados", ex);
            }
        }

        public async Task<List<CategoryBase>> GetByPaginationAsync(int pageNumber, int pageSize)
        {
            try
            {
                return await _context.Categories
                    .AsNoTracking()
                    .Where(c => c.Active == true)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar obter filtro do banco de dados", ex);
            }
        }

        public async Task<bool> UpdateAsync(CategoryBase category)
        {
            try
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar atualizar categoria", ex);
            }
        }
    }
}
