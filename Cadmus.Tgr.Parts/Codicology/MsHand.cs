using Fusi.Antiquity.Chronology;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Tgr.Parts.Codicology;

/// <summary>
/// A hand in a <see cref="MsScript"/>.
/// </summary>
public class MsHand
{
    /// <summary>
    /// Gets or sets the hand's identifier, a human-friendly, arbitrarily
    /// assigned short name.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    public HistoricalDate? Date { get; set; }

    /// <summary>
    /// Gets or sets this hand's start location in the manuscript.
    /// </summary>
    public MsLocation? Start { get; set; }

    /// <summary>
    /// Gets or sets this hand's end location in the manuscript.
    /// </summary>
    public MsLocation? End { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the description of abbreviations.
    /// </summary>
    public string? Abbreviations { get; set; }

    /// <summary>
    /// Gets or sets the letters.
    /// </summary>
    public List<MsHandLetter> Letters { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MsHand"/> class.
    /// </summary>
    public MsHand()
    {
        Letters = [];
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        StringBuilder sb = new(Id);

        if (Date is not null) sb.Append(": ").Append(Date);
        if (Start != null)
        {
            sb.Append(' ').Append(Start);
            if (End != null) sb.Append('-').Append(End);
        }

        return sb.ToString();
    }
}
