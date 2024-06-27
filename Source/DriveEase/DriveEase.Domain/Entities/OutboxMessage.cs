namespace DriveEase.Domain.Entities;

public record OutboxMessage
{
    /// <summary>
    /// gets indetifier
    /// </summary>
    /// <value>id</value>
    public Guid Id { get; init; }

    /// <summary>
    /// gets name
    /// </summary>
    /// <value>name</value>
    public string Name { get; init; }

    /// <summary>
    /// gets content
    /// </summary>
    /// <value>content</value>
    public string Content { get; init; }

    /// <summary>
    /// gets created date
    /// </summary>
    /// <value>created date</value>
    public DateTime? CreatedDate { get; init; }

    /// <summary>
    /// gets or sets proccessed date
    /// </summary>
    /// <value>proccessed date</value>
    public DateTime? ProccessedDate { get; set; } = null;

    /// <summary>
    /// gets or sets error
    /// </summary>
    /// <value>error</value>
    public string? Error { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="OutboxMessage"/> class.
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="content">contnet</param>
    /// <param name="createdDate">createddate</param>
    public OutboxMessage(string name, string content, DateTime? createdDate)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Content = content;
        this.CreatedDate = createdDate;
    }
}