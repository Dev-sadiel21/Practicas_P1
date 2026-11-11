
public class Tarea
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public Categoria Category { get; set; }
    public EstadoTarea Status { get; set; }

    private Tarea()
    {
    }

    public Tarea(string name, string description, Categoria category, EstadoTarea status)
    {
        Name = name;
        Description = description;
        Category = category;
        Status = status;
    }

    public void ModifyTask(string name, string description, Categoria category, EstadoTarea status)
    {
        Name = name;
        Description = description;
        Category = category;
        Status = status;
    }

    public void CompleteTask()
    {
        Status = EstadoTarea.Completada;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Nombre: {Name}");
        Console.WriteLine($"Descripción: {Description}");
        Console.WriteLine($"Categoría: {Category}");
        Console.WriteLine($"Estado: {Status}");
        Console.WriteLine();
    }
}