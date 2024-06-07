namespace DriveEase.Domain.Entities;

public record OutboxMessage
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Content { get; init; }
    public DateTime? CreatedDate { get; init; }
    public DateTime? ProccessedDate { get; set; } = null;
    public string? Error { get; set; } = string.Empty;

    public OutboxMessage(string name, string content, DateTime? createdDate)
    {
        Id = new Guid();
        Name = name;
        Content = content;
        CreatedDate = createdDate;
    }
}