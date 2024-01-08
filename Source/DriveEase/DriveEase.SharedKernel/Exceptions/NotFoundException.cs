namespace DriveEase.SharedKernel.Exceptions;

/// <summary>
/// NotFoundException exception class
/// </summary>
/// <seealso cref="System.Exception" />
public class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="key">The key.</param>
    public NotFoundException(string name, object key)
        : base($"{name} ({key}) was not found")
    {

    }
}
