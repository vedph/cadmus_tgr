using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadmus.Tgr.Parts.Grammar;

/// <summary>
/// A linguistically tagged form used by <see cref="LingTagsLayerFragment"/>.
/// </summary>
public class LingTaggedForm
{
    /// <summary>
    /// Gets or sets the optional lemmata, i.e. normalized forms.
    /// </summary>
    public List<string> Lemmata { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this form is dubious.
    /// </summary>
    public bool IsDubious { get; set; }

    /// <summary>
    /// Gets or sets an optional note about this form.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets the tags.
    /// </summary>
    public List<AnnotatedTag> Tags { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LingTaggedForm"/> class.
    /// </summary>
    public LingTaggedForm()
    {
        Lemmata = new List<string>();
        Tags = new List<AnnotatedTag>();
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

        if (Lemmata?.Count > 0)
        {
            sb.AppendJoin("; ", Lemmata).Append(": ");
        }
        if (Tags?.Count > 0)
        {
            sb.Append(string.Join(", ", Tags.Select(t => t.Value)));
        }

        return sb.Length > 0? sb.ToString() : base.ToString()!;
    }
}
