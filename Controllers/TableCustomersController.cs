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
    public class TableCustomersController : ControllerBase
    {
        private readonly KrutestContext _context;

        public TableCustomersController(KrutestContext context)
        {
            _context = context;
        }

        // GET: api/TableCustomers
        [HttpGet]
        public IEnumerable<object> GetTableCustomer()
        {
            var customers = from c in _context.TableCustomers
                            select new
                            {
                                c.CustId,
                                c.InitialCode,
                                initialName = _context.TableTitleNames.Where(x => x.InitialCode == c.InitialCode)
                                .Select(t => t.InitialName).FirstOrDefault(),
                                c.Name,
                                c.LastName,
                                c.CustType
                            };
            return customers;

        }

        // GET: api/TableCustomers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableCustomer([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tableCustomer = await _context.TableCustomers.FindAsync(id);

            if (tableCustomer == null)
            {
                return NotFound();
            }

            return Ok(tableCustomer);
        }

        // PUT: api/TableCustomers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTableCustomer([FromRoute] string id, [FromBody] TableCustomer tableCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tableCustomer.CustId)
            {
                return BadRequest();
            }

            _context.Entry(tableCustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableCustomerExists(id))
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

        // POST: api/TableCustomers
        [HttpPost]
        public async Task<IActionResult> PostTableCustomer([FromBody] TableCustomer tableCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TableCustomers.Add(tableCustomer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TableCustomerExists(tableCustomer.CustId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTableCustomer", new { id = tableCustomer.CustId }, tableCustomer);
        }

        // DELETE: api/TableCustomers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTableCustomer([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tableCustomer = await _context.TableCustomers.FindAsync(id);
            if (tableCustomer == null)
            {
                return NotFound();
            }

            _context.TableCustomers.Remove(tableCustomer);
            await _context.SaveChangesAsync();

            return Ok(tableCustomer);
        }

        private bool TableCustomerExists(string id)
        {
            return _context.TableCustomers.Any(e => e.CustId == id);
        }
    }
}