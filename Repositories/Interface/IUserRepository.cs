using BusinessEntities.RequestDto;

namespace Repositories.Interface
{
    public interface IUserRepository
    {
        Task<long> Add(UserRequest viewModel);
        Task<long> Update(UserRequest viewModel);
        Task<long> Delete(long Id);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(long Id);
    }
}
