using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Entities;

namespace TaskManager.Repository
{
    public interface ITaskRepository
    {
        void Add(TaskEntity item);
        void Delete(long id);
        IQueryable<TaskEntity> GetAll(TaskManagerQueryParameters queryParam);
        TaskEntity GetSingle(long id);
        bool Save();
        void Update(TaskEntity item);
    }
}
