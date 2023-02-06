namespace Cadmus.Tgr.Parts.Grammar;

/// <summary>
/// A source for a reading, used in <see cref="InterpolationsLayerFragment"/>.
/// </summary>
public class ReadingSource
{
    /// <summary>
    /// Gets or sets the witness.
    /// </summary>
    public string? Witness { get; set; }

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
        return Witness ?? base.ToString()!;
    }
}
