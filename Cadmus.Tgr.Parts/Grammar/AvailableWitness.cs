namespace Cadmus.Tgr.Parts.Grammar;

/// <summary>
/// A witness available in a specific context.
/// </summary>
public class AvailableWitness
{
    /// <summary>
    /// Gets or sets the witness identifier.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the coverage
    /// by this witness is only partial.
    /// </summary>
    public bool IsPartial { get; set; }

    /// <summary>
    /// Gets or sets a short note about this witness availability.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return Id + (IsPartial? "*" : "");
    }
}
