using Cadmus.Tgr.Parts.Codicology;
using Fusi.Antiquity.Chronology;
using System.Collections.Generic;

namespace Cadmus.Tgr.Parts.Codicology;

/// <summary>
/// Palimpsest sheets in a <see cref="MsUnit"/>.
/// </summary>
public class MsPalimpsest
{
    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    public HistoricalDate? Date { get; set; }

    /// <summary>
    /// Gets or sets an optional note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets the palimpsest location(s).
    /// </summary>
    public List<MsLocation> Locations { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MsPalimpsest"/> class.
    /// </summary>
    public MsPalimpsest()
    {
        Locations = [];
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"{Date}" +
            (Locations?.Count > 0 ? string.Join(", ", Locations) : "");
    }
}
