using System.Collections.Generic;
using System.Text;
using Fusi.Tools.Config;
using Cadmus.Core.Layers;
using Cadmus.Core;

namespace Cadmus.Tgr.Parts.Grammar
{
    /// <summary>
    /// Humanistic interpolations layer fragment.
    /// Tag: <c>fr.it.vedph.tgr.interp</c>.
    /// </summary>
    /// <seealso cref="ITextLayerFragment" />
    [Tag("fr.it.vedph.tgr.interp")]
    public sealed class InterpLayerFragment : ITextLayerFragment
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
        public List<InterpolationEntry> Entries { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterpLayerFragment"/>
        /// class.
        /// </summary>
        public InterpLayerFragment()
        {
            Entries = new List<InterpolationEntry>();
        }

        /// <summary>
        /// Get all the key=value pairs exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>fr.tag</c>=tag if any.</returns>
        public IEnumerable<DataPin> GetDataPins(IItem item = null)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            // fr-tot-count
            builder.Set(PartBase.FR_PREFIX + "tot", Entries?.Count ?? 0, false);

            if (Entries?.Count > 0)
            {
                foreach (InterpolationEntry entry in Entries)
                {
                    // fr-language
                    if (entry.Languages?.Length > 0)
                    {
                        builder.AddValues(PartBase.FR_PREFIX + "language",
                            entry.Languages);
                    }
                    // fr-value
                    builder.AddValue(PartBase.FR_PREFIX + "value", entry.Value,
                        filter: true);
                    // fr-tag
                    builder.AddValue(PartBase.FR_PREFIX + "tag", entry.Value);
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
            return new List<DataPinDefinition>(new[]
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
                    "M")
            });
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[Interp]");

            if (Entries?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var entry in Entries)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(entry);
                }
                if (Entries.Count > 3)
                    sb.Append("...(").Append(Entries.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
