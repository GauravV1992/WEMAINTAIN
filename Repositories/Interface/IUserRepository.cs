using BusinessEntities.RequestDto;

namespace Repositories.Interface
{
    public interface IUserRepository
    {
        Task<long> Add(UserRequest viewModel);
        Task<long> Update(UserRequest viewModel);
        Task<long> UpdateProfile(UserRequest viewModel);

        Task<long> UpdatePassword(UserRequest viewModel);
        Task<long> Delete(long Id);
        Task<IEnumerable<User>> GetAll(UserRequest request);
        Task<User> GetById(long Id);
        //Task<long> CheckUserLogin(UserRequest viewModel);
        Task<long> ForgetPassword(UserRequest viewModel);
    }
}
