using Cadmus.Philology.Parts;
using System.Collections.Generic;

namespace Cadmus.Tgr.Parts.Grammar
{
    /// <summary>
    /// A variant in a quotation, used in a <see cref="VarQuotation"/>.
    /// </summary>
    public class QuotationVariant
    {
        /// <summary>
        /// Gets or sets the lemma.
        /// </summary>
        public string? Lemma { get; set; }

        /// <summary>
        /// Gets or sets the entry type.
        /// </summary>
        public ApparatusEntryType Type { get; set; }

        /// <summary>
        /// Gets or sets the entry text value (zero for deletions).
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Gets or sets the witnesses reporting this variant.
        /// </summary>
        public List<AnnotatedValue> Witnesses { get; set; }

        /// <summary>
        /// Gets or sets the authors reporting or variously discussing
        /// this variant.
        /// </summary>
        public List<LocAnnotatedValue> Authors { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuotationVariant"/> class.
        /// </summary>
        public QuotationVariant()
        {
            Witnesses = new List<AnnotatedValue>();
            Authors = new List<LocAnnotatedValue>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{Type}] {Lemma}";
        }
    }
}
