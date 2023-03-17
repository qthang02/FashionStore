using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using API.Entities;
using API.Repositories;
using AutoMapper;

namespace API.Controllers;

public class CategoryController : BaseApiController
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepo, IMapper mapper)
    {
        _categoryRepo = categoryRepo;
        _mapper = mapper;
    }

    // GET: api/Category 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategory()
    {
        var category = await _categoryRepo.GetCategories();
        var categoryToReturn = _mapper.Map<IEnumerable<CategoryDto>>(category);
        return Ok(categoryToReturn);
    }

    // GET: api/Category/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var category = await _categoryRepo.GetCategoryById(id);

        return _mapper.Map<CategoryDto>(category);
    }
    
    // POST: api/Category
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> PostCategory(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        _categoryRepo.AddCategory(category);
        await _categoryRepo.SaveAllAsync();
        return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
    }
}

