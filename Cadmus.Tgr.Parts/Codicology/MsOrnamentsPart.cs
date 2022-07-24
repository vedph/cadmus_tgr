using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Tgr.Parts.Codicology
{
    /// <summary>
    /// Manuscript's ornamentations part.
    /// <para>Tag: <c>it.vedph.tgr.ms-ornaments</c>.</para>
    /// </summary>
    [Tag("it.vedph.tgr.ms-ornaments")]
    public sealed class MsOrnamentsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        public List<MsOrnament> Ornaments { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsOrnamentsPart"/> class.
        /// </summary>
        public MsOrnamentsPart()
        {
            Ornaments = new List<MsOrnament>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collection of pins with
        /// these keys: <c>type-{TYPE}-count</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem? item)
        {
            DataPinBuilder builder = new();

            builder.Set("tot", Ornaments?.Count ?? 0, false);

            if (Ornaments?.Count > 0)
            {
                foreach (MsOrnament ornament in Ornaments)
                {
                    // type-X-count counts if not null, unfiltered:
                    builder.Increase(ornament.Type, false, "type-");
                }
            }

            return builder.Build(this);
        }

        /// <summary>
        /// Gets the definitions of data pins used by the implementor.
        /// </summary>
        /// <returns>Data pins definitions.</returns>
        public override IList<DataPinDefinition> GetDataPinDefinitions()
        {
            return new List<DataPinDefinition>(new[]
            {
                new DataPinDefinition(DataPinValueType.Integer,
                    "tot-count",
                    "The total count of ornamentations."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "type-{TYPE}-count",
                    "The count of each ornamentation's type.")
            });
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

            sb.Append("[MsOrnaments]");

            if (Ornaments?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var entry in Ornaments)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(entry);
                }
                if (Ornaments.Count > 3)
                    sb.Append("...(").Append(Ornaments.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
