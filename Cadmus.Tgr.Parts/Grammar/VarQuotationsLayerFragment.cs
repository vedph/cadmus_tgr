using System.Collections.Generic;
using Fusi.Tools.Config;
using Cadmus.Core.Layers;
using Cadmus.Core;
using System.Text;

namespace Cadmus.Tgr.Parts.Grammar
{
    /// <summary>
    /// Variant-aware quotations layer fragment.
    /// Tag: <c>fr.it.vedph.tgr.var-quotations</c>.
    /// </summary>
    /// <seealso cref="ITextLayerFragment" />
    [Tag("fr.it.vedph.tgr.var-quotations")]
    public sealed class VarQuotationsLayerFragment : ITextLayerFragment
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
        public List<VarQuotation> Quotations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VarQuotationsLayerFragment"/>
        /// class.
        /// </summary>
        public VarQuotationsLayerFragment()
        {
            Location = "";
            Quotations = new List<VarQuotation>();
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
            builder.Set(PartBase.FR_PREFIX + "tot", Quotations?.Count ?? 0, false);

            if (Quotations?.Count > 0)
            {
                foreach (VarQuotation entry in Quotations)
                {
                    // fr-tag
                    builder.AddValue(PartBase.FR_PREFIX + "tag", entry.Tag);
                    // fr-authority
                    builder.AddValue(PartBase.FR_PREFIX + "authority",
                        entry.Authority);
                    // fr-work
                    builder.AddValue(PartBase.FR_PREFIX + "work", entry.Work);

                    if (entry.Variants?.Count > 0)
                    {
                        foreach (QuotationVariant variant in entry.Variants)
                        {
                            // fr-var-lemma (filtered)
                            builder.AddValue(PartBase.FR_PREFIX + "var-lemma",
                                variant.Lemma, filter: true);
                            // fr-var-value (filtered)
                            builder.AddValue(PartBase.FR_PREFIX + "var-value",
                                variant.Value, filter: true);
                        }
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
                    "The count of quotation entries."),
                new DataPinDefinition(DataPinValueType.String,
                    PartBase.FR_PREFIX + "tag",
                    "The list of quotation entry tags.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    PartBase.FR_PREFIX + "authority",
                    "The list of quotation entry authorities.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    PartBase.FR_PREFIX + "work",
                    "The list of quotation entry works.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    PartBase.FR_PREFIX + "var-lemma",
                    "The list of quotation entry variants lemmata.",
                    "MF"),
                new DataPinDefinition(DataPinValueType.String,
                    PartBase.FR_PREFIX + "var-value",
                    "The list of quotation entry variants values.",
                    "MF")
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

            sb.Append("[VarQuotations]");

            if (Quotations?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var entry in Quotations)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(entry);
                }
                if (Quotations.Count > 3)
                    sb.Append("...(").Append(Quotations.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
