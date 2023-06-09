﻿using BusinessEntities.RequestDto;

namespace Repositories.Interface
{
    public interface IServiceRepository
    {
        Task<long> Add(ServiceRequest viewModel);
        Task<long> Update(ServiceRequest viewModel);
        Task<long> Delete(long Id);
        Task<IEnumerable<Service>> GetAll();
        Task<Service> GetById(long Id);
    }
}
