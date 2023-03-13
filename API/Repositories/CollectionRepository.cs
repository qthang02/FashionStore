using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class CollectionRepository : ICollectionRepository
{
    private readonly ShopAppContext _context;
    private readonly IMapper _mapper;

    public CollectionRepository(ShopAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Collection>> GetCollections()
    {
        return await _context.Collections
            .Include(p => p.Products)
            .ToListAsync();
    }

    public async Task<Collection> GetCollection(int id)
    {
        return (await _context.Collections
            .Include(p => p.Products)
            .FirstOrDefaultAsync(p => p.CollectionId == id))!;
    }
}