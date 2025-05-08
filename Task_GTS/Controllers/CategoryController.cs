using Task_GTS.Models;
using Task_GTS.Repository;
using Task_GTS.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_GTS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _categoryRepository.Get(includeProps: [e => e.Products]).ToList();
            if (categories == null) return NotFound();
            return Ok(categories);
        }
        [HttpGet("Details/{categoryId}")]
        public ActionResult<Category> Details(int categoryId)
        {
            var category = _categoryRepository.GetOne(e => e.Id == categoryId);
            if (category == null) return NotFound();
            return Ok(category);
        }
        [HttpPost("Create")]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Create(category);
                _categoryRepository.Commit();
                return Created();
            }
            return BadRequest();
        }
        
        [HttpPut("Edit")]
        public IActionResult Edit(Category category)
        {
            var existing = _categoryRepository.GetOne(e => e.Id == category.Id,tracked:false);
            if (existing == null) return NotFound();

            _categoryRepository.Alter(category);
            _categoryRepository.Commit();
            return Ok(category);
        }
        [HttpDelete("{CateoryId}")]
        public IActionResult Delete(int CateoryId)
        {
            var category = _categoryRepository.GetOne(e => e.Id == CateoryId);
            if (category == null) return NotFound();

            _categoryRepository.Delete(category);
            _categoryRepository.Commit();
            return Ok(new { message = "Category deleted successfully" });
        }
    }
}
