using Microsoft.EntityFrameworkCore;

public class GestorTarea
{
    private AppDbContext context = new AppDbContext();

    public bool HasTasks()
    {
        return context.Tareas.Any();
    }

    public List<Tarea> GetAll()
    {
        return context.Tareas
            .Include(t => t.Category)
            .OrderBy(t => t.Id)
            .ToList();
    }

    public Tarea AddTask(string name, string description, Categoria category, EstadoTarea status)
    {
        Tarea task = new Tarea(name, description, GetOrCreateCategory(category), status);
        context.Tareas.Add(task);
        context.SaveChanges();
        return task;
    }

    public Tarea FindById(int id)
    {
        return context.Tareas
            .Include(t => t.Category)
            .FirstOrDefault(t => t.Id == id);
    }

    public List<Tarea> FindByName(string name)
    {
        return context.Tareas
            .Include(t => t.Category)
            .Where(t => t.Name.Contains(name))
            .ToList();
    }

    public List<Tarea> FindByCategory(string category)
    {
        return context.Tareas
            .Include(t => t.Category)
            .Where(t => t.Category.Name.Contains(category))
            .ToList();
    }

    public List<Tarea> FindByStatus(EstadoTarea status)
    {
        return context.Tareas
            .Include(t => t.Category)
            .Where(t => t.Status == status)
            .ToList();
    }

    public bool TaskExists(string name, string category)
    {
        return context.Tareas.Any(t => t.Name == name && t.Category.Name == category);
    }

    public bool ModifyTask(int id, string name, string description, Categoria category, EstadoTarea status)
    {
        Tarea task = FindById(id);

        if (task == null)
        {
            return false;
        }

        task.ModifyTask(name, description, GetOrCreateCategory(category), status);
        context.SaveChanges();
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
        context.SaveChanges();
        return true;
    }

    public bool DeleteTask(int id)
    {
        Tarea task = FindById(id);

        if (task == null)
        {
            return false;
        }

        context.Tareas.Remove(task);
        context.SaveChanges();
        return true;
    }

    private Categoria GetOrCreateCategory(Categoria category)
    {
        Categoria existing = context.Categorias.FirstOrDefault(c => c.Name == category.Name);

        if (existing != null)
        {
            return existing;
        }

        return category;
    }
}