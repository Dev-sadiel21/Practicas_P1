using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_de_Tareas_Personales_Tarea_3_
{
    public class GestorTarea
    {

        private Dictionary<int, Tarea> tasks = new Dictionary<int, Tarea>();
        private int nextId = 1;

        public bool HasTasks()
        {
            return tasks.Count > 0;
        }

        public List<Tarea> GetAll()
        {
            return tasks.Values.ToList();
        }

        public Tarea AddTask(string name, string description, Categoria category, EstadoTarea status)
        {
            Tarea task = new Tarea(nextId, name, description, category, status);
            tasks.Add(task.Id, task);
            nextId++;
            return task;
        }

        public Tarea FindById(int id)
        {
            if (tasks.ContainsKey(id))
            {
                return tasks[id];
            }
            return null;
        }

        public List<Tarea> FindByName(string name)
        {
            List<Tarea> result = new List<Tarea>();

            foreach (Tarea task in tasks.Values)
            {
                if (task.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(task);
                }
            }
            return result;
        }

        public List<Tarea> FindByCategory(string category)
        {
            List<Tarea> result = new List<Tarea>();

            foreach (Tarea task in tasks.Values)
            {
                if (task.Category.Name.Contains(category, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(task);
                }
            }
            return result;
        }

        public List<Tarea> FindByStatus(EstadoTarea status)
        {
            List<Tarea> result = new List<Tarea>();

            foreach (Tarea task in tasks.Values)
            {
                if (task.Status == status)
                {
                    result.Add(task);
                }
            }
            return result;
        }

        public bool ModifyTask(int id, string name, string description, Categoria category, EstadoTarea status)
        {
            Tarea task = FindById(id);

            if (task == null)
            {
                return false;
            }

            task.ModifyTask(name, description, category, status);
            return true;
        }

        public bool CompleteTask(int id)
        {
            Tarea task = FindById(id);

            if (task == null)
            {
                return false;
            }

            task.CompleteTask();
            return true;
        }

        public bool DeleteTask(int id)
        {
            if (!tasks.ContainsKey(id))
            {
                return false;
            }

            tasks.Remove(id);
            return true;
        }

    }
}
