
public class Categoria
{
    public int Id { get; set; }
    public string Name { get; set; }

    private Categoria()
    {
    }

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