using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_5_GC_Car_Dealership.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capstone_5_GC_Car_Dealership.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly CarDbContext _context;

        public CarController(CarDbContext context)
        {
            _context = context;
            if (_context.Cars.Count() == 0)
            {
                _context.Cars.Add(new Cars { Make = "Ford", Model = "Focus", Year = 2019, Color = "Blue" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cars>>> GetCars()
        {
            var carsList = await _context.Cars.ToListAsync();
            return carsList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cars>> GetCarById(int id)
        {
            var found = await _context.Cars.FindAsync(id);

            if (found == null)
            {
                return NotFound();
            }
            return found;
        }

        [HttpPost]
        public async Task<ActionResult<Cars>> PostCar(Cars cars)
        {
            _context.Cars.Add(cars);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCarById), new { id = cars.Id }, cars);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cars>> PutCar(int id, Cars cars)
        {
            if (id != cars.Id)
            {
                return BadRequest();
            }

            _context.Entry(cars).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cars>> DeleteCar(int id)
        {
            var cars = await _context.Cars.FindAsync(id);
            if (cars == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(cars);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}