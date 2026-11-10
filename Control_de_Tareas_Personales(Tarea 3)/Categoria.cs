using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_de_Tareas_Personales_Tarea_3_
{
    public class Categoria
    {
        public string Name { get; set; }

        public Categoria(string name)
        {
            Name = name;
        }

        public void ModifyCategory(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}