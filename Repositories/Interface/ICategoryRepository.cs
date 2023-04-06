using BusinessEntities.RequestDto;

namespace Repositories.Interface
{
    public interface ICategoryRepository
    {
        //long Add(CategoryRequest viewModel);
        //long Update(CategoryRequest viewModel);
        //long Delete(long Id);
 Task<IEnumerable<Package>> GetAll();
        //Package GetById(long Id);
    }
}
