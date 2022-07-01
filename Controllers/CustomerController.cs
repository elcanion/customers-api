using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PloomesTest.Models;
using PloomesTest.Data;

namespace PloomesTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDb _context;

        public CustomerController(CustomerDb context)
        {
            _context = context;
        }

        // GET /customers
        /// <summary>
        /// Lists all registered customers.
        /// </summary>
        /// <returns>All customers</returns>
        /// <response code="200">Returns all customers</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<Customer>>> Read()
        {
            var result = await _context.Customers.ToListAsync();
            return Ok(result);
        }

        // GET /customer/{id}
        /// <summary>
        /// Find a specific customer.
        /// </summary>
        /// <param name="id">Required parameter</param>
        /// <returns>A new customer</returns>
        /// <response code="200">Returns a specific Customer</response>
        /// <response code="400">Customer couldn't be found</response>  
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Read(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return BadRequest("Customer not found.");
            return Ok(customer);
        }
          
        // POST /customer
        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <remarks>
        /// Example:
        ///
        ///     POST /customer
        ///     {
        ///        "id": 0,
        ///        "name": "Yuki",
        ///        "email": "yuki@gmail.com",
        ///        "phone": "(61)99999-9999",
        ///     }
        ///
        /// </remarks>
        /// <returns>A new customer</returns>
        /// <response code="201">Customer created successfully</response>
        /// <response code="400">Customer couldn't be created</response>
        /// <response code="500">Probably an insertion error</response>  
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<List<Customer>>> Create(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            return Created(new Uri($"{Request.Path}/customer.Id", UriKind.Relative), customer);
        }

        // PUT /customer
        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <remarks>
        /// Example:
        ///
        ///     PUT /customer
        ///     {
        ///        "id": 0,
        ///        "name": "Yuki",
        ///        "email": "yuki@gmail.com",
        ///        "phone": "(61)99999-9999",
        ///     }
        ///
        /// </remarks>
        /// <returns>A new customer</returns>
        /// <response code="200">Returns all customers</response>
        /// <response code="400">Couldn't find customer</response>
        /// <response code="500">Probably an update error</response> 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult<List<Customer>>> Update(Customer toUpdate)
        {
            var customer = await _context.Customers.FindAsync(toUpdate.Id);
            if (customer is null) return BadRequest("Couldn't find customer.");
            customer.Name = toUpdate.Name;
            customer.Email = toUpdate.Email;
            customer.Phone = toUpdate.Phone;
            await _context.SaveChangesAsync();
            return Ok(await _context.Customers.ToListAsync());
        }

        // DELETE /customer/{id}
        /// <summary>
        /// Deletes a specific customer.
        /// </summary>
        /// <param name="id">Required parameter</param>
        /// <returns>A new customer</returns>
        /// <response code="200">Returns all customers</response>
        /// <response code="400">Couldn't find customer</response>  
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Customer>>> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer is null) return BadRequest("Couldn't find customer.");
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok(await _context.Customers.ToListAsync());
        }
        
    }

}