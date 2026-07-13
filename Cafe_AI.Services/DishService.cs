using Cafe_AI.Core.Entities;
using Cafe_AI.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_AI.Services
{
    public class DishService
    {
        private readonly AppDbContext _context;
        public DishService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dish>> GetMenuDishesAsync()
        {
            return await _context.Dishes.Where(d => d.IsApproved).ToListAsync();
        }

        public async Task<List<Dish>> GetPendingDishesAsyng()
        {
            return await _context.Dishes.Where(d => !d.IsApproved && !d.IsRejected).ToListAsync();
        }

        public async Task<Dish> CreateDishAsync(Dish dish)
        {
            dish.Id = Guid.NewGuid();
            dish.CreatedAt = DateTime.UtcNow;
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
            return dish;
        }

        public async Task ApproveDishAsync(Guid dishId)
        {
            var dish = await _context.Dishes.FindAsync(dishId);
            if (dish != null)
            {
                dish.IsApproved = true;
                dish.IsRejected = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task RejectDishAsync(Guid dishId)
        {
            var dish = await _context.Dishes.FindAsync(dishId);
            if (dish != null)
            {
                dish.IsApproved = false;
                dish.IsRejected = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
