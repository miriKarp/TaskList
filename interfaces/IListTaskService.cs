using Task.models;
using System.Collections.Generic;

namespace Task.interfaces
{
    public interface IListTaskService
    {
        List<task> GetAll();
        task Get(int id);
        void Add(task task);
        void Delete(int id);
        void Update(task task);
        int Count {get;}
    }
}