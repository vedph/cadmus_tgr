using System.Collections.Generic;
using System.Text;

namespace Cadmus.Tgr.Parts.Grammar;

/// <summary>
/// Quotation with variants, used in a <see cref="VarQuotationsLayerFragment"/>.
/// </summary>
public class VarQuotation
{
    /// <summary>
    /// Gets or sets the tag.
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// Gets or sets the authority type (grammatical, linguistic).
    /// </summary>
    public string? Authority { get; set; }

    /// <summary>
    /// Gets or sets the work.
    /// </summary>
    public string? Work { get; set; }

    /// <summary>
    /// Gets or sets the location.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets an optional note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets further occurrences of the same quotation in other works.
    /// </summary>
    public List<QuotationParallel> Parallels { get; set; }

    /// <summary>
    /// Gets or sets the variant readings in this quotation. These are
    /// located only implicitly via their lemma.
    /// </summary>
    public List<QuotationVariant> Variants { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="VarQuotation"/>
    /// class.
    /// </summary>
    public VarQuotation()
    {
        Parallels = new List<QuotationParallel>();
        Variants = new List<QuotationVariant>();
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        StringBuilder sb = new();

        if (!string.IsNullOrEmpty(Tag))
            sb.Append('[').Append(Tag).Append("] ");

        sb.Append(Work);

        if (!string.IsNullOrEmpty(Location))
            sb.Append(' ').Append(Location);

        if (Parallels?.Count > 0) sb.Append(" P=").Append(Parallels.Count);
        if (Variants?.Count > 0) sb.Append(" V=").Append(Variants.Count);

        return sb.ToString();
    }
}
