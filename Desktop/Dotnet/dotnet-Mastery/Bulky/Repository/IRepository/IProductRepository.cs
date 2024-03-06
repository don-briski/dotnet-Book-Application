using Bulky.Models;
using Bulky.Repository.IRepository;


namespace Bulky.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
       // void Save();
    }
};
