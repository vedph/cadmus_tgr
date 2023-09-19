using Cadmus.Refs.Bricks;
using System.Collections.Generic;

namespace Cadmus.Tgr.Parts.Codicology;

/// <summary>
/// Manuscript's content used by <see cref="MsContentsPart"/>.
/// </summary>
public class MsContent
{
    /// <summary>
    /// Gets or sets the start.
    /// </summary>
    public MsLocation? Start { get; set; }

    /// <summary>
    /// Gets or sets the end.
    /// </summary>
    public MsLocation? End { get; set; }

    /// <summary>
    /// Gets or sets the author and work conventional citation.
    /// </summary>
    public string? Work { get; set; }

    /// <summary>
    /// Gets or sets the location in the work.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the incipit.
    /// </summary>
    public string? Incipit { get; set; }

    /// <summary>
    /// Gets or sets the explicit.
    /// </summary>
    public string? Explicit { get; set; }

    /// <summary>
    /// Gets or sets the note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets the editions.
    /// </summary>
    public List<DocReference> Editions { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MsContent"/> class.
    /// </summary>
    public MsContent()
    {
        Editions = new List<DocReference>();
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"{Start}-{End}: {Work}, {Location}";
    }
}
