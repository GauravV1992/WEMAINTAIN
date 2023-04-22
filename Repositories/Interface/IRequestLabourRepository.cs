using BusinessEntities.RequestDto;

namespace Repositories.Interface
{
    public interface IRequestLabourRepository
    {
        Task<IEnumerable<RequestLabour>> GetAll(RequestLabourRequest request);
        Task<RequestLabour> GetById(long Id);
    }
}
