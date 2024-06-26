﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        public Task<List<Walk>> GetWalksAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,int pageNumber = 1, int pageSize = 1000);
        public Task<Walk> GetWalkAsync(Guid id);
        public Task<Walk> CreateWalkAsync(Walk walk);
        public Task<Walk> UpdateWalkAsync(Guid id, Walk walk);
        public Task<Walk> DeleteWalkAsync(Guid id);
    }
}
