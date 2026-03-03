using BeerApi.Models;
using BeerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace BeerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            try
            {
                var customers = await _customerService.GetAllAsync();
                return Ok(customers);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Service error fetching customers");
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status502BadGateway, title: "Database service error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error fetching customers");
                return Problem(detail: "An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError, title: "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(id);
                if (customer == null) return NotFound();
                return Ok(customer);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Service error fetching customer {Id}", id);
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status502BadGateway, title: "Database service error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error fetching customer {Id}", id);
                return Problem(detail: "An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError, title: "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var created = await _customerService.CreateAsync(customer);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Service error creating customer");
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status502BadGateway, title: "Database service error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating customer");
                return Problem(detail: "An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError, title: "Internal server error");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var updated = await _customerService.UpdateAsync(id, customer);
                if (!updated) return NotFound();
                return NoContent();
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Service error updating customer {Id}", id);
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status502BadGateway, title: "Database service error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating customer {Id}", id);
                return Problem(detail: "An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError, title: "Internal server error");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _customerService.DeleteAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Service error deleting customer {Id}", id);
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status502BadGateway, title: "Database service error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting customer {Id}", id);
                return Problem(detail: "An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError, title: "Internal server error");
            }
        }
    }
}
