using System.Collections.Generic;

namespace Cadmus.Tgr.Parts.Codicology;

/// <summary>
/// A guard sheet in a <see cref="MsUnit"/>.
/// </summary>
public class MsGuardSheet
{
    /// <summary>
    /// Gets or sets a value indicating whether this instance is a back
    /// guard sheet (true) or a front one (false).
    /// </summary>
    public bool IsBack { get; set; }

    /// <summary>
    /// Gets or sets the material.
    /// </summary>
    public string? Material { get; set; }

    /// <summary>
    /// Gets or sets the note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets the watermarks.
    /// </summary>
    public List<MsWatermark> Watermarks { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MsGuardSheet"/> class.
    /// </summary>
    public MsGuardSheet()
    {
        Watermarks = new List<MsWatermark>();
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"[{(IsBack ? "B" : "F")}] {Material}";
    }
}
