using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Configuration;

namespace Cadmus.Tgr.Parts.Codicology;

/// <summary>
/// Manuscript's formal features of writing part.
/// <para>Tag: <c>it.vedph.tgr.ms-formal-features</c>.</para>
/// </summary>
[Tag("it.vedph.tgr.ms-formal-features")]
public sealed class MsFormalFeaturesPart : PartBase
{
    /// <summary>
    /// Gets or sets the entries.
    /// </summary>
    public List<MsFormalFeature> Features { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MsFormalFeaturesPart"/> class.
    /// </summary>
    public MsFormalFeaturesPart()
    {
        Features = [];
    }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>tot-count</c> and a collection of pins with
    /// these keys: <c>hand-id</c>.</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item)
    {
        DataPinBuilder builder = new();

        builder.Set("tot", Features?.Count ?? 0, false);

        if (Features?.Count > 0)
        {
            foreach (MsFormalFeature feature in Features)
                builder.AddValue("hand-id", feature.HandId);
        }

        return builder.Build(this);
    }

    /// <summary>
    /// Gets the definitions of data pins used by the implementor.
    /// </summary>
    /// <returns>Data pins definitions.</returns>
    public override IList<DataPinDefinition> GetDataPinDefinitions()
    {
        return [.. new[]
        {
            new DataPinDefinition(DataPinValueType.Integer,
                "tot-count",
                "The total count of features."),
            new DataPinDefinition(DataPinValueType.String,
                "hand-id",
                "The hand IDs.",
                "M")
        }];
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

        sb.Append("[MsFormalFeatures]");

        if (Features?.Count > 0)
        {
            sb.Append(' ');
            int n = 0;
            foreach (var entry in Features)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append("; ");
                sb.Append(entry);
            }
            if (Features.Count > 3)
                sb.Append("...(").Append(Features.Count).Append(')');
        }

        return sb.ToString();
    }
}
