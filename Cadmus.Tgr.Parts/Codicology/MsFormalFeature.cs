namespace Cadmus.Tgr.Parts.Codicology;

/// <summary>
/// A formal feature of a manuscript's writing, included in a
/// <see cref="MsFormalFeaturesPart"/>.
/// </summary>
public class MsFormalFeature
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the hand identifier.
    /// </summary>
    public string? HandId { get; set; }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"[{HandId}] " +
            (Description?.Length > 60
            ? Description[..60] + "..."
            : Description);
    }
}
