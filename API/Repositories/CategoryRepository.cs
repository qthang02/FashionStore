using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ShopAppContext _context;
    private readonly IMapper _mapper;

    public CategoryRepository(ShopAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _context.Categories
            .Include(p => p.Products)
            .ToListAsync();
    }

    public async Task<Category> GetCategoryById(int id)
    {
        return (await _context.Categories
            .Include(p => p.Products)
            .FirstOrDefaultAsync(p => p.CategoryId == id))!;
    }

    public void AddCategory(Category category)
    {
        _context.Categories.Add(category);
    }
    
    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}