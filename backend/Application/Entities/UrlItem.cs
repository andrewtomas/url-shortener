namespace Application.Entities;

public class UrlItem
{
    public UrlItem()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public string Url { get; set; }
    public string ShortUrl { get; set; }
    public DateTime CreationTime { get; }
    public Guid GroupId { get; set; }
    public UrlGroup Group { get; set; }
}