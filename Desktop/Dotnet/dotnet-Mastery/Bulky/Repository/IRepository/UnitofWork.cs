namespace Bulky.Repository.IRepository
{
    public interface IUnitofWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        void Save();
    }
};

