using Cadmus.Core;
using Cadmus.Core.Layers;
using Fusi.Tools.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Cadmus.Tgr.Parts.Grammar;

/// <summary>
/// Available witnesses fragment.
/// Tag: <c>fr.it.vedph.tgr.available-witnesses</c>.
/// </summary>
[Tag("fr.it.vedph.tgr.available-witnesses")]
public sealed class AvailableWitnessesLayerFragment : ITextLayerFragment
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
    /// Gets or sets the witnesses.
    /// </summary>
    public List<AvailableWitness> Witnesses { get; set; }

    public AvailableWitnessesLayerFragment()
    {
        Location = "";
        Witnesses = new List<AvailableWitness>();
    }

    /// <summary>
    /// Get all the key=value pairs exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>Pins.</returns>
    public IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new();

        builder.Set(PartBase.FR_PREFIX + "tot", Witnesses?.Count ?? 0, false);

        if (Witnesses?.Count > 0)
        {
            builder.AddValues(PartBase.FR_PREFIX + "witness",
                from w in Witnesses select w.Id);
        }

        return builder.Build(null);
    }

    /// <summary>
    /// Gets the definitions of data pins used by the implementor.
    /// </summary>
    /// <returns>
    /// Data pins definitions.
    /// </returns>
    public IList<DataPinDefinition> GetDataPinDefinitions()
    {
        return new List<DataPinDefinition>(new[]
        {
            new DataPinDefinition(DataPinValueType.Integer,
               PartBase.FR_PREFIX + "tot-count",
               "The total count of entries."),
            new DataPinDefinition(DataPinValueType.String,
               PartBase.FR_PREFIX + "witness",
               "The list of witnesses IDs.",
               "M")
        });
    }
}
