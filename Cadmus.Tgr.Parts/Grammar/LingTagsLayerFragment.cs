using Fusi.Tools.Config;
using System.Collections.Generic;
using Cadmus.Core.Layers;
using Cadmus.Core;
using System.Text;
using System.Linq;

namespace Cadmus.Tgr.Parts.Grammar
{
    /// <summary>
    /// Linguistic tags fragment.
    /// Tag: <c>fr.it.vedph.tgr.ling-tags</c>.
    /// </summary>
    /// <seealso cref="ITextLayerFragment" />
    [Tag("fr.it.vedph.tgr.ling-tags")]
    public sealed class LingTagsLayerFragment : ITextLayerFragment
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
        /// The tagged forms.
        /// </summary>
        public List<LingTaggedForm> Forms { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LingTagsLayerFragment"/>
        /// class.
        /// </summary>
        public LingTagsLayerFragment()
        {
            Location = "";
            Forms = new List<LingTaggedForm>();
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

            // fr.tot-count
            builder.Set(PartBase.FR_PREFIX + "tot", Forms?.Count ?? 0, false);

            if (Forms?.Count > 0)
            {
                foreach (LingTaggedForm form in Forms)
                {
                    // fr.lemma
                    if (form.Lemmata?.Count > 0)
                    {
                        builder.AddValues(PartBase.FR_PREFIX + "lemma",
                            form.Lemmata,
                            filter: true,
                            filterOptions: true);
                    }

                    // fr.tag
                    if (form.Tags?.Count > 0)
                    {
                        builder.AddValues(PartBase.FR_PREFIX + "tag",
                            form.Tags.Select(t => t.Value!));
                    }
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
                    "The total count of forms."),
                new DataPinDefinition(DataPinValueType.String,
                    PartBase.FR_PREFIX + "lemma",
                    "A lemma.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    PartBase.FR_PREFIX + "tag",
                    "A tag.",
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
            StringBuilder sb = new();

            sb.Append("[LingTags]");

            if (Forms?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var entry in Forms)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(entry);
                }
                if (Forms.Count > 3)
                    sb.Append("...(").Append(Forms.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
