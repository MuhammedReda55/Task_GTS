using Task_GTS.Models;
using Task_GTS.Models.DTOs;
using Task_GTS.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_GTS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var products = _productRepository.Get(includeProps: [e => e.Category]);
            if (products == null) return NotFound();
            return Ok(products.ToList());
        }

        [HttpPost("Create")]
        public IActionResult Create(ProductWithDtos productWithDtos)
        {
          

            if (ModelState.IsValid)
            {
                string fileName = "default.jpg";
                if (productWithDtos.file != null && productWithDtos.file.Length > 0)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(productWithDtos.file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        productWithDtos.file.CopyTo(stream);
                    }
                }

                
                var product = new Product
                {
                    Name = productWithDtos.Name,
                    Description = productWithDtos.Description,
                    Price = productWithDtos.Price,
                    Quantity = productWithDtos.Quantity,
                    CategoryId = productWithDtos.CategoryId,
                    Img = fileName,
                    Rate = 0 
                };

                _productRepository.Create(product);
                _productRepository.Commit();

                Response.Cookies.Append("message", "Product added successfully");
                return Created();
            }

            return BadRequest(ModelState);
        }
        
        [HttpGet("Details")]
        public IActionResult Details(int productId)
        {
            var product = _productRepository.GetOne(e => e.Id == productId);

            if (product == null) return NotFound();

            return Ok(product);
        }
        [HttpPut("Edit")]
        public IActionResult Edit(ProductWithDtos productWithDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldProduct = _productRepository.GetOne(e => e.Id == productWithDtos.Id, tracked: false);
            if (oldProduct == null)
                return NotFound("Product not found");

            string newImageName = oldProduct.Img;

            
            if (productWithDtos.file != null && productWithDtos.file.Length > 0)
            {
                
                newImageName = Guid.NewGuid().ToString() + Path.GetExtension(productWithDtos.file.FileName);
                var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", newImageName);

                using (var stream = System.IO.File.Create(newImagePath))
                {
                    productWithDtos.file.CopyTo(stream);
                }

                if (!string.IsNullOrEmpty(oldProduct.Img) && oldProduct.Img != "default.jpg")
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", oldProduct.Img);
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }
            }

            
            var updatedProduct = new Product
            {
                Id = productWithDtos.Id,
                Name = productWithDtos.Name,
                Description = productWithDtos.Description,
                Price = productWithDtos.Price,
                Quantity = productWithDtos.Quantity,
                Rate = oldProduct.Rate, 
                CategoryId = productWithDtos.CategoryId,
                Img = newImageName
            };

            _productRepository.Alter(updatedProduct);
            _productRepository.Commit();

            return Ok("Product updated successfully");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int productId)
        {
            if (ModelState.IsValid)
            {


                var product = _productRepository.GetOne(e => e.Id == productId);
                // Delete old img

                if (product == null) return NotFound();

                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "Images", product.Img);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                _productRepository.Delete(product);

                _productRepository.Commit();

                return Ok();
            }
            return BadRequest();
        }


    }
}
