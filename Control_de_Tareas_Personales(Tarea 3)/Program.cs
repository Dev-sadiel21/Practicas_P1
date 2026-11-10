
class Program
{
    static GestorTarea gestor = new GestorTarea();

    static void Main()
    {

        int option;

        do
        {
            DisplayMenu();

            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Error: debes ingresar un número del menú.");
                continue;
            }

            try
            {
                switch (option)
                {
                    case 1:
                        {
                            AddTask();
                            break;
                        }

                    case 2:
                        {
                            ModifyTask();
                            break;
                        }

                    case 3:
                        {
                            CompleteTask();
                            break;
                        }

                    case 4:
                        {
                            FindTask();
                            break;
                        }

                    case 5:
                        {
                            ListTasks();
                            break;
                        }

                    case 6:
                        {
                            DeleteTask();
                            break;
                        }

                    case 7:
                        {
                            Console.WriteLine("Saliendo del programa...");
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Opción no válida.");
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error: " + ex.Message);
            }

        } while (option != 7);
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
                Console.WriteLine("Error: este campo no puede estar vacío.");
            }
            else if (!value.Any(char.IsLetter))
            {
                Console.WriteLine("Error: debes ingresar un texto válido.");
                value = "";
            }

        } while (string.IsNullOrWhiteSpace(value));

        return value.Trim();
    }

    static EstadoTarea ReadStatus()
    {
        Console.WriteLine("Seleccione el estado:");
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

        return (EstadoTarea)statusOption;
    }

    static void ShowList(List<Tarea> list, string notFoundMessage)
    {
        if (list.Count == 0)
        {
            Console.WriteLine(notFoundMessage);
            return;
        }

        foreach (Tarea task in list)
        {
            task.ShowInfo();
        }
    }

    static void AddTask()
    {
        Console.WriteLine("\nAgregar tarea");

        string name = ReadValidText("Nombre de la tarea: ");
        string description = ReadValidText("Descripción: ");
        Categoria category = new Categoria(ReadValidText("Categoría: "));
        EstadoTarea status = ReadStatus();

        Tarea task = gestor.AddTask(name, description, category, status);

        Console.WriteLine("Tarea agregada correctamente.");
        Console.WriteLine($"ID de la tarea: {task.Id}");
    }

    static void ModifyTask()
    {
        Console.WriteLine("\nModificar tarea");

        if (!gestor.HasTasks())
        {
            Console.WriteLine("No hay tareas registradas para modificar.");
            return;
        }

        Console.Write("Ingrese el ID de la tarea: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        if (gestor.FindById(id) == null)
        {
            Console.WriteLine("No existe una tarea con ese ID.");
            return;
        }

        string name = ReadValidText("Nuevo nombre: ");
        string description = ReadValidText("Nueva descripción: ");
        Categoria category = new Categoria(ReadValidText("Nueva categoría: "));
        EstadoTarea status = ReadStatus();

        gestor.ModifyTask(id, name, description, category, status);
        Console.WriteLine("Tarea modificada correctamente.");
    }

    static void CompleteTask()
    {
        Console.WriteLine("\nCompletar tarea");

        if (!gestor.HasTasks())
        {
            Console.WriteLine("No hay tareas registradas para completar.");
            return;
        }

        Console.Write("Ingrese el ID de la tarea que desea completar: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        if (gestor.CompleteTask(id))
        {
            Console.WriteLine("Tarea completada correctamente.");
        }
        else
        {
            Console.WriteLine("No existe una tarea con ese ID.");
        }
    }

    static void FindTask()
    {
        Console.WriteLine("\nBuscar tareas");

        if (!gestor.HasTasks())
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
                {
                    SearchId();
                    break;
                }

            case 2:
                {
                    string name = ReadValidText("Ingrese el nombre (o parte del nombre): ");
                    ShowList(gestor.FindByName(name), "No se encontraron tareas con ese nombre.");
                    break;
                }

            case 3:
                {
                    string category = ReadValidText("Ingrese la categoría: ");
                    ShowList(gestor.FindByCategory(category), "No se encontraron tareas en esa categoría.");
                    break;
                }

            case 4:
                {
                    EstadoTarea status = ReadStatus();
                    ShowList(gestor.FindByStatus(status), "No se encontraron tareas con ese estado.");
                    break;
                }

            case 5:
                {
                    return;
                }

            default:
                {
                    Console.WriteLine("Opción no válida.");
                    break;
                }
        }
    }

    static void SearchId()
    {
        Console.Write("Ingrese el ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Tarea task = gestor.FindById(id);

        if (task != null)
        {
            task.ShowInfo();
        }
        else
        {
            Console.WriteLine("No existe una tarea con este ID.");
        }
    }

    static void ListTasks()
    {
        Console.WriteLine("\nLista de tareas");
        ShowList(gestor.GetAll(), "No hay tareas registradas.");
    }

    static void DeleteTask()
    {
        Console.WriteLine("\nEliminar tarea");

        if (!gestor.HasTasks())
        {
            Console.WriteLine("No hay tareas registradas para eliminar.");
            return;
        }

        Console.Write("Ingrese el ID de la tarea que desea eliminar: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        if (gestor.DeleteTask(id))
        {
            Console.WriteLine("Tarea eliminada correctamente.");
        }
        else
        {
            Console.WriteLine("No existe una tarea con ese ID.");
        }
    }
}