using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Repositories;

public interface ICollectionRepository
{
    Task<IEnumerable<Collection>> GetCollections();
    Task<Collection> GetCollection(int id);
}