using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NursingHomeResidents.Data;
using NursingHomeResidents.Models;

namespace NursingHomeResidents.Controllers
{
    /// <summary>
    /// API endpoints for managing nursing home residents.
    /// </summary>
    [Route("api/residents")]
    [ApiController]
    public class ResidentsController : ControllerBase
    {
        private readonly NursingHomeContext _context;

        public ResidentsController(NursingHomeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all residents.
        /// </summary>
        /// <returns>A list of residents.</returns>
        [HttpGet]
        public IActionResult GetResidents()
        {
            var residents = _context.Residents.ToList();
            return Ok(residents);
        }

        /// <summary>
        /// Get a resident by ID.
        /// </summary>
        /// <param name="id">The ID of the resident to retrieve.</param>
        /// <returns>The resident with the specified ID.</returns>
        [HttpGet("{id}")]
        public IActionResult GetResident(int id)
        {
            var resident = _context.Residents.Find(id);

            if (resident == null)
            {
                return NotFound();
            }

            return Ok(resident);
        }

        /// <summary>
        /// Create a new resident.
        /// </summary>
        /// <param name="resident">The resident to create.</param>
        /// <returns>The created resident.</returns>
        [HttpPost]
        public IActionResult PostResident([FromBody] Resident resident)
        {
            _context.Residents.Add(resident);
            _context.SaveChanges();

            return CreatedAtAction("GetResident", new { id = resident.ResidentID }, resident);
        }

        /// <summary>
        /// Update an existing resident by their ID.
        /// </summary>
        /// <param name="id">The unique ID of the resident to update.</param>
        /// <param name="resident">The updated resident information.</param>
        /// <returns>No content if the update is successful, BadRequest if the provided ID doesn't match the resident, or NotFound if the resident with the specified ID is not found.</returns>
        [HttpPut("{id}")]
        public IActionResult PutResident(int id, [FromBody] Resident resident)
        {
            if (id != resident.ResidentID)
            {
                return BadRequest();
            }
            _context.Entry(resident).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidentExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        /// <summary>
        /// Delete a resident by ID.
        /// </summary>
        /// <param name="id">The ID of the resident to delete.</param>
        /// <returns>No content if successful, or an error if the resident doesn't exist.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteResident(int id)
        {
            var resident = _context.Residents.Find(id);
            if (resident == null)
            {
                return NotFound();
            }

            _context.Residents.Remove(resident);
            _context.SaveChanges();

            return NoContent();
        }

        private bool ResidentExists(int id)
        {
            return _context.Residents.Any(e => e.ResidentID == id);
        }
    }
}
