using ApiColmadoSinDB.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiColmadoSinDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryMovementController : ControllerBase
    {
        private static List<Provider> _provider = new List<Provider>
        {
            new Provider{IdProvider=1,ProviderName="Fernandez SRL", ContactName="Juan Fernandez", Address="calle x",City="Distrito Nacional", Country="RD", Phone="809-555-6666"},
            new Provider{IdProvider=2,ProviderName="Supermercados Bravo", ContactName="Carla Diaz", Address="jacobo majluta",City="SDN", Country="RD", Phone="809-666-5555"},
            new Provider{IdProvider=3,ProviderName="Ramirez Motors", ContactName="Pedro Ramirez", Address="avenida la Paz",City="Santiago", Country="RD", Phone="829-999-0000"}
        };
        [HttpGet]
        public ActionResult<IEnumerable<Provider>> GetAll()
        {
            return Ok(_provider);
        }
        [HttpGet("{id}")]
        public ActionResult<Provider> GetById(int id)
        {
            var provider = _provider.FirstOrDefault(p => p.IdProvider == id);
            if (provider == null)
            {
                return NotFound($"El Id {id} no existe");
            }
            return Ok(provider);
        }
        [HttpPost]
        public ActionResult<Provider> Create(Provider provider)
        {
            if (string.IsNullOrWhiteSpace(provider.ProviderName))
            {
                return NotFound("El nombre del proveedor es obligatorio");
            }
            var newId = _provider.Any() ? _provider.Max(p => provider.IdProvider) + 1 : 1;
            provider.IdProvider = newId;
            _provider.Add(provider);
            return CreatedAtAction(
                nameof(GetById),
                new { id = provider.IdProvider },
                provider);
        }
        [HttpPut]
        public ActionResult<Provider> Update(int id, Provider provider)
        {
            var UpdateProvider = _provider.FirstOrDefault(p => p.IdProvider == id);
            if (provider == null)
            {
                return NotFound($"El Id {id} no existe");
            }
            UpdateProvider.ProviderName = provider.ProviderName;
            UpdateProvider.ContactName = provider.ContactName;
            UpdateProvider.Address = provider.Address;
            UpdateProvider.City = provider.City;
            UpdateProvider.Country = provider.Country;
            UpdateProvider.Phone = provider.Phone;
            return Ok(UpdateProvider);
        }
        [HttpDelete]
        public ActionResult SoftDelete(int id)
        {
            var provider = _provider.FirstOrDefault(p => p.IdProvider == id);
            if (provider == null)
            {
                return NotFound($"El Id {id} no existe");
            }
            provider.IsDelete = '1';
            return Ok(provider);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var provider = _provider.FirstOrDefault(p => p.IdProvider == id);
            if (provider == null)
            {
                return NotFound($"El Id {id} no existe");
            }
            _provider.Remove(provider);
            return NoContent();
        }
        [HttpGet("WhithCondition")]
        public ActionResult<IEnumerable<Provider>> GetAllWhithCondition()
        {
            return Ok(_provider.Where(p => p.IsDelete == '0'));
        }
    }
}
