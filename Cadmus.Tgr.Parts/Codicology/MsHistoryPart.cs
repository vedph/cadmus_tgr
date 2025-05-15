using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Configuration;

namespace Cadmus.Tgr.Parts.Codicology;

/// <summary>
/// Manuscript's history part.
/// <para>Tag: <c>it.vedph.tgr.ms-history</c>.</para>
/// </summary>
[Tag("it.vedph.tgr.ms-history")]
public sealed class MsHistoryPart : PartBase
{
    /// <summary>
    /// Gets or sets the provenances.
    /// </summary>
    public List<GeoAddress> Provenances { get; set; }

    /// <summary>
    /// Gets or sets the history.
    /// </summary>
    public string? History { get; set; }

    /// <summary>
    /// Gets or sets the owners.
    /// </summary>
    public List<string> Owners { get; set; }

    /// <summary>
    /// Gets or sets the subscription.
    /// </summary>
    public MsSubscription? Subscription { get; set; }

    /// <summary>
    /// Gets or sets the annotations.
    /// </summary>
    public List<MsAnnotation> Annotations { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MsHistoryPart"/> class.
    /// </summary>
    public MsHistoryPart()
    {
        Provenances = [];
        Owners = [];
        Annotations = [];
    }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: multiple pins with key <c>area</c>,
    /// <c>owner</c> (both filtered, with digits).</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item)
    {
        DataPinBuilder builder = new(
            new StandardDataPinTextFilter());

        if (Provenances?.Count > 0)
        {
            builder.AddValues("area", Provenances.Select(p => p.Area!),
                filter: true, filterOptions: true);
        }

        if (Owners?.Count > 0)
        {
            builder.AddValues("owner", Owners,
                filter: true, filterOptions: true);
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
            new DataPinDefinition(DataPinValueType.String,
               "area",
               "The list of manuscript's provenance areas.",
               "Mf"),
            new DataPinDefinition(DataPinValueType.String,
               "owner",
               "The list of manuscript's owners.",
               "Mf")
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

        sb.Append("[MsHistory]");

        if (Provenances?.Count > 0)
        {
            sb.Append(' ')
              .AppendJoin("; ", from p in Provenances
                                select p.ToString());
        }

        return sb.ToString();
    }
}
