
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
