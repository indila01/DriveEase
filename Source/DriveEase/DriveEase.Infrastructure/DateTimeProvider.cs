using DriveEase.SharedKernel.Util;

namespace DriveEase.Infrastructure;

/// <summary>
/// Represents the machine date time service.
/// </summary>
/// <seealso cref="DriveEase.SharedKernel.Util.IDateTime" />
internal sealed class DateTimeProvider : IDateTime
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}
