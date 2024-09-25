using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProjectApi.Data;
using MyProjectApi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Car>>> SearchCars([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                // If no query is provided, return all cars
                return await _context.Cars.Include(c => c.Photo).ToListAsync();
            }
            query = query.ToLower();
            // Perform a case-insensitive search on both Name and Status
            var filteredCars = await _context.Cars
            .Include(c => c.Photo)
            .Where(c => c.Name.ToLower().Contains(query) || c.Status.ToLower().Contains(query))
            .ToListAsync();

            return Ok(filteredCars);
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.Include(c => c.Photo).FirstOrDefaultAsync(c => c.ID == id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // POST: api/Cars
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            // Handle photo creation if provided
            if (car.Photo != null)
            {
                var photo = new Photo { Base64 = car.Photo.Base64 };
                _context.Photos.Add(photo);
                await _context.SaveChangesAsync();
                car.PhotoID = photo.ID;
            }

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCar), new { id = car.ID }, car);
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            // Check if the car exists in the database
            var existingCar = await _context.Cars.Include(c => c.Photo).FirstOrDefaultAsync(c => c.ID == id);
            if (existingCar == null)
            {
                return NotFound();
            }

            // Update the existing car's details
            existingCar.Name = car.Name;
            existingCar.Status = car.Status;

            // Handle photo update if provided
            if (car.Photo != null)
            {
                if (existingCar.Photo != null)
                {
                    // Update existing photo
                    existingCar.Photo.Base64 = car.Photo.Base64;
                    _context.Entry(existingCar.Photo).State = EntityState.Modified;
                }
                else
                {
                    // Add new photo if none existed before
                    var newPhoto = new Photo { Base64 = car.Photo.Base64 };
                    _context.Photos.Add(newPhoto);
                    await _context.SaveChangesAsync();
                    existingCar.PhotoID = newPhoto.ID;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(existingCar);
        }


        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            // Handle photo deletion
            if (car.PhotoID != null)
            {
                var photo = await _context.Photos.FindAsync(car.PhotoID);
                if (photo != null)
                {
                    _context.Photos.Remove(photo);
                }
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.ID == id);
        }
    }
}
