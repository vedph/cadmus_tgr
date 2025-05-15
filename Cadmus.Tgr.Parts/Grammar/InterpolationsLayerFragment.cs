using System.Collections.Generic;
using System.Text;
using Fusi.Tools.Configuration;
using Cadmus.Core.Layers;
using Cadmus.Core;

namespace Cadmus.Tgr.Parts.Grammar;

/// <summary>
/// Humanistic interpolations layer fragment.
/// Tag: <c>fr.it.vedph.tgr.interpolations</c>.
/// </summary>
/// <seealso cref="ITextLayerFragment" />
[Tag("fr.it.vedph.tgr.interpolations")]
public sealed class InterpolationsLayerFragment : ITextLayerFragment
{
    /// <summary>
    /// Gets or sets the location of this fragment.
    /// </summary>
    /// <remarks>
    /// The location can be expressed in different ways according to the
    /// text coordinates system being adopted. For instance, it might be a
    /// simple token-based coordinates system (e.g. 1.2=second token of
    /// first block), or a more complex system like an XPath expression.
    /// </remarks>
    public string Location { get; set; }

    /// <summary>
    /// Gets or sets the entries.
    /// </summary>
    public List<Interpolation> Interpolations { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InterpolationsLayerFragment"/>
    /// class.
    /// </summary>
    public InterpolationsLayerFragment()
    {
        Location = "";
        Interpolations = [];
    }

    /// <summary>
    /// Get all the key=value pairs exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>fr.tag</c>=tag if any.</returns>
    public IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new(
            new StandardDataPinTextFilter());

        // fr-tot-count
        builder.Set(PartBase.FR_PREFIX + "tot",
            Interpolations?.Count ?? 0, false);

        if (Interpolations?.Count > 0)
        {
            foreach (Interpolation entry in Interpolations)
            {
                // fr-language
                if (entry.Languages?.Count > 0)
                {
                    builder.AddValues(PartBase.FR_PREFIX + "language",
                        entry.Languages);
                }
                // fr-value
                builder.AddValue(PartBase.FR_PREFIX + "value", entry.Value,
                    filter: true);
                // fr-tag
                builder.AddValue(PartBase.FR_PREFIX + "tag", entry.Tag);
                // fr-role
                builder.AddValue(PartBase.FR_PREFIX + "role", entry.Role);
            }
        }

        return builder.Build(null);
    }

    /// <summary>
    /// Gets the definitions of data pins used by the implementor.
    /// </summary>
    /// <returns>Data pins definitions.</returns>
    public IList<DataPinDefinition> GetDataPinDefinitions()
    {
        return [.. new[]
        {
            new DataPinDefinition(DataPinValueType.Integer,
                PartBase.FR_PREFIX + "tot-count",
                "The entries count."),
            new DataPinDefinition(DataPinValueType.String,
                PartBase.FR_PREFIX + "language",
                "The list of languages.",
                "M"),
            new DataPinDefinition(DataPinValueType.String,
                PartBase.FR_PREFIX + "value",
                "The list of values.",
                "MF"),
            new DataPinDefinition(DataPinValueType.String,
                PartBase.FR_PREFIX + "tag",
                "The list of tags.",
                "M"),
            new DataPinDefinition(DataPinValueType.String,
                PartBase.FR_PREFIX + "role",
                "The list of roles.",
                "M")
        }];
    }

    /// <summary>
    /// Returns a <see cref="string" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        StringBuilder sb = new();

        sb.Append("[Interp]");

        if (Interpolations?.Count > 0)
        {
            sb.Append(' ');
            int n = 0;
            foreach (var entry in Interpolations)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append("; ");
                sb.Append(entry);
            }
            if (Interpolations.Count > 3)
                sb.Append("...(").Append(Interpolations.Count).Append(')');
        }

        return sb.ToString();
    }
}
