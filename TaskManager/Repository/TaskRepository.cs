using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Entities;
using System.Linq.Dynamic.Core;

namespace TaskManager.Repository
{
    public class TaskRepository:ITaskRepository
    {
        private TaskManagerDbContext _context;

        public TaskRepository(TaskManagerDbContext context)
        {
            _context = context;
        }

        public IQueryable<TaskEntity> GetAll(TaskManagerQueryParameters taskQueryParameters)
        {
            IQueryable<TaskEntity> _allTasks = _context.Tasks.OrderBy(taskQueryParameters.OrderBy, taskQueryParameters.Descending);

            if (taskQueryParameters.HasQuery)
            {
                _allTasks = _allTasks
                    .Where(x => x.Title.ToLowerInvariant().Contains(taskQueryParameters.Query.ToLowerInvariant())
                    || x.Title.ToLowerInvariant().Contains(taskQueryParameters.Query.ToLowerInvariant()));
            }

            return _allTasks.OrderBy(x => x.Title).Skip(taskQueryParameters.PageCount * (taskQueryParameters.Page - 1)).Take(taskQueryParameters.PageCount);            
        }

        public TaskEntity GetSingle(long id)
        {
            return _context.Tasks.FirstOrDefault(x => x.TaskId == id);
        }

        public void Add(TaskEntity item)
        {
            _context.Tasks.Add(item);
        }

        public void Delete(long id)
        {
            TaskEntity task = GetSingle(id);
            _context.Tasks.Remove(task);
        }

        public void Update(TaskEntity item)
        {
            _context.Tasks.Update(item);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
