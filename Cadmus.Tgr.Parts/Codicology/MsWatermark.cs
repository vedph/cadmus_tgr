namespace Cadmus.Tgr.Parts.Codicology;

/// <summary>
/// A watermark in a <see cref="MsGuardSheet"/> or <see cref="MsUnit"/>.
/// </summary>
public class MsWatermark
{
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"{Value}: " + (Description?.Length > 60
            ? Description[..60] + "..." : Description);
    }
}
