using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Entities;

namespace TaskManager.Services
{
    public class SeedDataService : ISeedDataService
    {
        private TaskManagerDbContext _context;

        public SeedDataService(TaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task EnsureSeedData()
        {
            _context.Database.EnsureCreated();

            _context.Tasks.RemoveRange(_context.Tasks);
            _context.SaveChanges();

            var task = new TaskEntity
            {
                Title = "Read a book",
                TaskDetails = "Read Sadguru books"                
            };

            _context.Add(task);

            var task2 = new TaskEntity
            {
                Title = "Go to temple",
                TaskDetails = "Go to temple"
            };

            _context.Add(task2);

            await _context.SaveChangesAsync();
        }

    }
}
