using Task_GTS.Data;
using Task_GTS.Models;
using Task_GTS.Repository.IRepository;

namespace Task_GTS.Repository
{
    public class CategoryRepository : Repositroy<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
