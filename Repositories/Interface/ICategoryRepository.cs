using BusinessEntities.RequestDto;

namespace Repositories.Interface
{
    public interface ICategoryRepository
    {
        long Add(CategoryRequest viewModel);
        long Update(CategoryRequest viewModel);
        long Delete(long Id);
        IEnumerable<DBCategory> GetAll();
        DBCategory GetById(long Id);
    }
}
