using ApiColmadoSinDB.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiColmadoSinDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> _products = new List<Product>
        {
            new Product{IdProduct=1, ProductName="Arroz Selecto", Price=220,QualityPerUnit="20",Stock=100,CategoryId=1},
            new Product{IdProduct=2, ProductName="Cloro", Price=50,QualityPerUnit="5",Stock=30,CategoryId=2},
            new Product{IdProduct=3, ProductName="Coolant", Price=700,QualityPerUnit="20",Stock=40,CategoryId=3}
        };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(_products);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.IdProduct == id);
            if (product == null)
            {
                return NotFound($"El id {id} Ingresado no existe");
            }
            return Ok(product);
        }
        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                return NotFound("El nombre del producto es obligatorio");
            }
            var newId = _products.Any() ? _products.Max(p => product.IdProduct) + 1 : 1;
            product.IdProduct = newId;
            _products.Add(product);
            return CreatedAtAction(
                nameof(GetById),
                new { id = product.IdProduct },
                product);
        }
        [HttpPut]
        public ActionResult<Product> Update(int id, Product product)
        {
            var updateProduct = _products.FirstOrDefault(p => p.IdProduct == id);
            if (updateProduct == null)
            {
                return NotFound($"El Id {id} no exite");
            }
            updateProduct.ProductName = product.ProductName;
            updateProduct.Price = product.Price;
            updateProduct.QualityPerUnit = product.QualityPerUnit;
            updateProduct.Stock = product.Stock;
            updateProduct.CategoryId = product.CategoryId;
            return Ok(updateProduct);
        }

        [HttpDelete]
        public ActionResult SoftDelete(int id)
        {
            var product = _products.FirstOrDefault(p => p.IdProduct == id);
            if (product == null)
            {
                return NotFound($"El Id {id} no existe");
            }
            product.IsDelete = '1';
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.IdProduct == id);
            if (product == null)
            {
                return NotFound($"El Id {id} no existe");
            }
            _products.Remove(product);
            return NoContent();
        }
        [HttpGet("GetWhithCondition")]
        public ActionResult<IEnumerable<Product>> GetAllWithCondition()
        {
            return Ok(_products.Where(c => c.IsDelete == '0'));
        }
    }
}
