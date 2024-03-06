using Bulky.Models;
using Bulky.Repository.IRepository;


namespace Bulky.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
       // void Save();
    }
};
