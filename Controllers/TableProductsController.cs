using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test1.Model;

namespace Test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableProductsController : ControllerBase
    {
        private readonly KrutestContext _context;

        public TableProductsController(KrutestContext context)
        {
            _context = context;
        }

        // GET: api/TableProducts
        [HttpGet]
        public IEnumerable<object> GetTableProduct()
        {

            var products = from p in _context.TableProducts
                          select new                        
                           {
                               p.Code,
                               p.Name,
                               p.Description,
                               p.Price,
                               p.UnitPerPrice,
                               p.Qty,
                               p.Status,  
                               p.UnitCode,
                               unitName = _context.TableUnits.Where(t => t.UnitCode == p.UnitCode).Select(t => t.UnitName)
                                   .FirstOrDefault(),
                               catName = _context.TableCategorys.Where(t => t.CatId == p.CatId).Select(t => t.CatName)
                                   .FirstOrDefault()

                           };

            return products;
        }

        // GET: api/TableProducts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableProduct([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tableProduct = await _context.TableProducts.FindAsync(id);

            if (tableProduct == null)
            {
                return NotFound();
            }

            return Ok(tableProduct);
        }

        // PUT: api/TableProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTableProduct([FromRoute] string id, [FromBody] TableProduct tableProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tableProduct.Code)
            {
                return BadRequest();
            }

            _context.Entry(tableProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TableProducts
        [HttpPost]
        public async Task<IActionResult> PostTableProduct([FromBody] TableProduct tableProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TableProducts.Add(tableProduct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TableProductExists(tableProduct.Code))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTableProduct", new { id = tableProduct.Code }, tableProduct);
        }

        // DELETE: api/TableProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTableProduct([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tableProduct = await _context.TableProducts.FindAsync(id);
            if (tableProduct == null)
            {
                return NotFound();
            }

            _context.TableProducts.Remove(tableProduct);
            await _context.SaveChangesAsync();

            return Ok(tableProduct);
        }

        private bool TableProductExists(string id)
        {
            return _context.TableProducts.Any(e => e.Code == id);
        }
    }
}