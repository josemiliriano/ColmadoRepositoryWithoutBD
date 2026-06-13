using ApiColmadoSinDB.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiColmadoSinDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private static List<Category> _categories = new List<Category>
        {
            new Category{IdCategory=1, CategoryName="Alimentos", Description="Alimentos de consumo humano"},
            new Category{IdCategory=2, CategoryName="Limpieza", Description="Productos de limpieza"},
            new Category{IdCategory=3, CategoryName="Vehiculo", Description="Productos para consumo automotriz"},
        };
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            return Ok(_categories);
        }
        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {
            var Category = _categories.FirstOrDefault(m => m.IdCategory == id);
            if (Category == null)
            {
                return NotFound($"No existe el ID {id}");
            }
            return Category;
        }

        [HttpPost]
        public ActionResult<Category> Create(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return BadRequest("El nombre de la categoria es requerido");
            }
            var newId = _categories.Any() ? _categories.Max(c => c.IdCategory) + 1 : 1;
            category.IdCategory = newId;

            _categories.Add(category);

            return CreatedAtAction(
                nameof(GetById),
                new { id = category.IdCategory },
                category);
        }

        [HttpPut]
        public ActionResult<Category> Update(int id, Category category)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.IdCategory == id);

            if (existingCategory == null)
            {
                return NotFound("No existe una categoría con ese ID.");
            }

            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return BadRequest("El nombre de la categoría es requerido.");
            }

            existingCategory.CategoryName = category.CategoryName;
            existingCategory.Description = category.Description;

            return Ok(existingCategory);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var category = _categories.FirstOrDefault(c => c.IdCategory == id);

            if (category == null)
            {
                return NotFound("No existe una categoría con ese ID.");
            }

            _categories.Remove(category);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult SoftDelete(int id)
        {
            var category = _categories.FirstOrDefault(c => c.IdCategory == id);

            if (category == null)
            {
                return NotFound("No existe una categoría con ese ID.");
            }

            category.IsDelete = '1';

            return NoContent();
        }
        [HttpGet("GetWhithCondition")]
        public ActionResult<IEnumerable<Category>> GetAllWithCondition()
        {
            return Ok(_categories.Where(c => c.IsDelete == '0'));
        }
    }
}
