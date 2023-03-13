using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Repositories;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Controllers;

public class CollectionController : BaseApiController
{
    private readonly ICollectionRepository _collectionRepository;
    private readonly IMapper _mapper;

    public CollectionController(ICollectionRepository collectionRepository, IMapper mapper)
    {
        _collectionRepository = collectionRepository;
        _mapper = mapper;
    }

    // GET: api/Collection
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CollectionDto>>> GetAllCollections()
    {
        var collections = await _collectionRepository.GetCollections();
        var CollectionsToReturn = _mapper.Map<IEnumerable<CollectionDto>>(collections);
        return Ok(CollectionsToReturn);
    }

    // GET: api/Collection/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CollectionDto>> GetCollection(int id)
    {
        var collection = await _collectionRepository.GetCollection(id);

        return _mapper.Map<CollectionDto>(collection);
    }
}

