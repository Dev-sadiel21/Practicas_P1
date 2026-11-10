
class Program
{


    static Dictionary<int, string> tasks = new Dictionary<int, string>();
    static Dictionary<int, string> descriptions = new Dictionary<int, string>();
    static Dictionary<int, string> categories = new Dictionary<int, string>();
    static Dictionary<int, string> statuses = new Dictionary<int, string>();

    static List<int> taskIds = new List<int>();

    static void Main()
    {
        int Option;


        do
        {
            DisplayMenu();

            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out Option))
            {
                Console.WriteLine("Error: debes ingresar un número del menú.");
                continue;
            }

            try
            {
                switch (Option)
                {
                    case 1:
                        AddTasks();
                        break;

                    case 2:
                        ModifyTask();
                        break;

                    case 3:
                        CompleteTaks();
                        break;

                    case 4:
                        FindTaks();
                        break;

                    case 5:
                        ListTaks();
                        break;

                    case 6:
                        DeleteTaks();
                        break;

                    case 7:
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error " + ex.Message);
            }

        } while (Option != 7);
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\nSistema de Control de Tareas Personales");
        Console.WriteLine("1. Agregar tarea");
        Console.WriteLine("2. Modificar tarea");
        Console.WriteLine("3. Completar tarea");
        Console.WriteLine("4. Buscar tarea");
        Console.WriteLine("5. Lista de tareas");
        Console.WriteLine("6. Eliminar tarea");
        Console.WriteLine("7. Salir del programa");
    }

    static string ReadValidText(string message)
    {
        string value;

        do
        {
            Console.Write(message);
            value = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Error este campo no puede estar vacío.");
            }
            else if (!value.Any(char.IsLetter))
            {
                Console.WriteLine("Error debes ingresar un texto válido.");
                value = "";
            }

        } while (string.IsNullOrWhiteSpace(value));

        return value.Trim();
    }

    static void AddTasks()
    {
        Console.WriteLine("\nAgregar tarea");

        string taskName = ReadValidText("Nombre de la tarea: ");
        string taskDescription = ReadValidText("Descripción: ");
        string taskCategory = ReadValidText("Categoría: ");

        Console.WriteLine("Seleccione el estado");
        Console.WriteLine("1. Pendiente");
        Console.WriteLine("2. Completada");

        int statusOption;

        do
        {
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out statusOption) ||
                (statusOption != 1 && statusOption != 2))
            {
                Console.WriteLine("Opción no válida.");
                statusOption = 0;
            }

        } while (statusOption == 0);

        string tasStatus = statusOption == 1 ? "Pendiente" : "Completada";

        int taskId = tasks.Count + 1;

        while (tasks.ContainsKey(taskId))
        {
            taskId++;
        }

        tasks.Add(taskId, taskName);
        descriptions.Add(taskId, taskDescription);
        categories.Add(taskId, taskCategory);
        statuses.Add(taskId, tasStatus);
        taskIds.Add(taskId);

        Console.WriteLine("Tarea agregada correctamente.");
        Console.WriteLine($"ID de la tarea: {taskId}");
    }

    static void ModifyTask()
    {
        Console.WriteLine("\nModificar tarea");

        if (tasks.Count == 0)
        {
            Console.WriteLine("No hay tareas registradas para modificar.");
            return;
        }

        Console.Write("Ingrese el ID de la tarea: ");

        if (!int.TryParse(Console.ReadLine(), out int taskId))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        if (tasks.ContainsKey(taskId))
        {
            tasks[taskId] = ReadValidText("Nuevo nombre: ");

            descriptions[taskId] = ReadValidText("Nueva descripción: ");

            categories[taskId] = ReadValidText("Nueva categoría: ");

            Console.WriteLine("Seleccione el nuevo estado:");
            Console.WriteLine("1. Pendiente");
            Console.WriteLine("2. Completada");

            int statusOption;

            do
            {
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out statusOption) ||
                    (statusOption != 1 && statusOption != 2))
                {
                    Console.WriteLine("Opción no válida.");
                    statusOption = 0;
                }

            } while (statusOption == 0);

            statuses[taskId] = statusOption == 1 ? "Pendiente" : "Completada";

            Console.WriteLine("Tarea modificada correctamente.");
        }
        else
        {
            Console.WriteLine("No existe una tarea con ese ID.");
        }
    }

    static void CompleteTaks()
    {
        Console.WriteLine("\nCompletar tarea");

        if (tasks.Count == 0)
        {
            Console.WriteLine("No hay tareas registradas para completar.");
            return;
        }

        Console.Write("Ingrese el ID de la tarea que desea completar: ");

        if (!int.TryParse(Console.ReadLine(), out int taskId))
        {
            Console.WriteLine("ID invalido.");
            return;
        }

        if (!tasks.ContainsKey(taskId))
        {
            Console.WriteLine("No existe una tarea con ese ID.");
            return;
        }

        statuses[taskId] = "Completada";

        Console.WriteLine("Tarea completada correctamente.");
    }

    static void FindTaks()
    {
        Console.WriteLine("\nBuscar tareas");

        if (tasks.Count == 0)
        {
            Console.WriteLine("No hay tareas registradas.");
            return;
        }

        Console.WriteLine("1. Buscar por ID");
        Console.WriteLine("2. Buscar por nombre");
        Console.WriteLine("3. Buscar por categoría");
        Console.WriteLine("4. Buscar por estado");
        Console.WriteLine("5. Volver");

        Console.Write("Seleccione una opción: ");

        if (!int.TryParse(Console.ReadLine(), out int option))
        {
            Console.WriteLine("Opción inválida.");
            return;
        }

        switch (option)
        {
            case 1:
                SearchId();
                break;

            case 2:
                SearchName();
                break;

            case 3:
                SearchCategory();
                break;

            case 4:
                SearchStatues();
                break;

            case 5:
                return;

            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }

    static void SearchId()
    {
        Console.Write("Ingrese el ID: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (tasks.ContainsKey(id))
            {
                ShowTask(id);
            }
            else
            {
                Console.WriteLine("No existe una tarea con este ID.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido.");
        }
    }

    static void SearchName()
    {
        string name = ReadValidText("Ingrese el nombre (o parte del nombre): ");
        bool found = false;

        foreach (int id in taskIds)
        {
            if (tasks[id].Contains(name, StringComparison.OrdinalIgnoreCase))
            {
                ShowTask(id);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No se encontraron tareas con ese nombre.");
        }
    }

    static void SearchCategory()
    {
        string category = ReadValidText("Ingrese la categoría: ");
        bool found = false;

        foreach (int id in taskIds)
        {
            if (categories[id].Contains(category, StringComparison.OrdinalIgnoreCase))
            {
                ShowTask(id);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No se encontraron tareas en esa categoría.");
        }
    }

    static void SearchStatues()
    {
        string status = ReadValidText("Ingrese el estado (Pendiente / Completada): ");
        bool found = false;

        foreach (int id in taskIds)
        {
            if (statuses[id].Contains(status, StringComparison.OrdinalIgnoreCase))
            {
                ShowTask(id);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No se encontraron tareas con ese estado.");
        }
    }

    static void ShowTask(int id)
    {
        Console.WriteLine("\nInformación de la tarea");
        Console.WriteLine($"ID: {id}");
        Console.WriteLine($"Nombre: {tasks[id]}");
        Console.WriteLine($"Descripción: {descriptions[id]}");
        Console.WriteLine($"Categoría: {categories[id]}");
        Console.WriteLine($"Estado: {statuses[id]}");
    }

    static void ListTaks()
    {
        Console.WriteLine("\nLista de tareas");

        if (taskIds.Count == 0)
        {
            Console.WriteLine("No hay tareas registradas.");
            return;
        }

        foreach (int taskId in taskIds)
        {
            Console.WriteLine($"ID: {taskId}");
            Console.WriteLine($"Nombre: {tasks[taskId]}");
            Console.WriteLine($"Descripción: {descriptions[taskId]}");
            Console.WriteLine($"Categoría: {categories[taskId]}");
            Console.WriteLine($"Estado: {statuses[taskId]}");
            Console.WriteLine("");
        }
    }

    static void DeleteTaks()
    {
        Console.WriteLine("\nEliminar tarea");

        if (tasks.Count == 0)
        {
            Console.WriteLine("No hay tareas registradas para eliminar.");
            return;
        }

        Console.Write("Ingrese el ID de la tarea que desea eliminar: ");

        if (!int.TryParse(Console.ReadLine(), out int taskId))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        if (!tasks.ContainsKey(taskId))
        {
            Console.WriteLine("No existe una tarea con ese ID.");
            return;
        }

        tasks.Remove(taskId);
        descriptions.Remove(taskId);
        categories.Remove(taskId);
        statuses.Remove(taskId);
        taskIds.Remove(taskId);

        Console.WriteLine("Tarea eliminada correctamente.");
    }
}



