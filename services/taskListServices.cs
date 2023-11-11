using Task.models;
using Task.interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;


 namespace Task.services{


    public class taskListServices : IListTaskService
    {

      List<task> tasks {get;}

      private IWebHostEnvironment webHost;
        private string filePath;
        public taskListServices(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "data", "taskList.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                tasks = JsonSerializer.Deserialize<List<task>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
        }

      public List<task> GetAll() => tasks;
      
      public task Get(int id) => tasks.FirstOrDefault(t => t.id == id);
              
        public void Add(task task)
        {
            task.id = tasks.Count() + 1;
            tasks.Add(task);
            saveToFile();
                      
        } 
        public void Delete(int id){
            var task =Get(id);
            if(task is null)
               return;
            tasks.Remove(task);
            saveToFile();
            
        }
        public void Update(task task)
        {
            var index = tasks.FindIndex(t => t.id == task.id);
            if (index == -1)
                return;

            tasks[index] = task;
            saveToFile();
         }

        public int Count => tasks.Count();
    }
 }
 