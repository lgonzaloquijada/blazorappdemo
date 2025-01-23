namespace blazorappdemo.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }

    public Category()
    {
        Name = string.Empty;
        Image = string.Empty;
    }
}