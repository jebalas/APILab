using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICapstone.Context;
using APICapstone.Models;

namespace APICapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public ProductController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> ProductID(int id)
        {
            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // GET: api/Product/Category/5
        [HttpGet("Category/{id}")]
        public async Task<ActionResult<IEnumerable<Products>>> GetCategory(int id)
        {
            var products = await _context.Products.Where(c => c.CategoryId == id).ToListAsync();

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // GET: api/Product/Supplier/5
        [HttpGet("Supplier/{id}")]
        public async Task<ActionResult<IEnumerable<Products>>> GetSupplier(int id)
        {
            var products = await _context.Products.Where(s => s.SupplierId == id).ToListAsync();

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // GET: api/Product/MaxPrice/7
        [HttpGet("MaxPrice/{mprice}")]
        public async Task<ActionResult<IEnumerable<Products>>> GetMaxPrice(decimal mprice)
        {
            var products = await _context.Products.Where(p => p.UnitPrice <= mprice).ToListAsync();

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // GET: api/Product/Discontinued/true
        [HttpGet("Discontinued/{discont}")]
        public async Task<ActionResult<IEnumerable<Products>>> GetDiscontinued(bool discont)
        {
            var products = await _context.Products.Where(p => p.Discontinued == true).ToListAsync();

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // GET: api/Product/Discontinued/false
        [HttpGet("ExcludeDiscontinued/{discont}")]
        public async Task<ActionResult<IEnumerable<Products>>> ExcludeDiscontinued(bool discont)
        {
            var products = await _context.Products.Where(p => p.Discontinued == false).ToListAsync();

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
            if (id != products.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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

        // POST: api/Product
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = products.ProductId }, products);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return products;
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
