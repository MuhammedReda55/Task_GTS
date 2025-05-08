using Task_GTS.Data;
using Task_GTS.Models;
using Task_GTS.Repository.IRepository;

namespace Task_GTS.Repository
{
    public class ProductRepository : Repositroy<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
