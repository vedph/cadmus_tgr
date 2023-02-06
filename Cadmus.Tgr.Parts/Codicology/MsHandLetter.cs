namespace Cadmus.Tgr.Parts.Codicology;

/// <summary>
/// A letter or other symbol described for a given <see cref="MsHand"/>.
/// </summary>
public class MsHandLetter
{
    /// <summary>
    /// Gets or sets the letter or other symbol being described.
    /// </summary>
    public string? Letter { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the optional image identifier. This is the prefix
    /// shared by any number of image resources representing this item.
    /// </summary>
    public string? ImageId { get; set; }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"{Letter}: " +
            (Description?.Length > 60
            ? Description[..60] + "..."
            : Description);
    }
}
