namespace Application.Entities;

public class UrlGroup
{
    public UrlGroup()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public string Name { get; set; }
    public List<UrlItem> URLItems { get; } = new();
}