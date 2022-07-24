using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Tgr.Parts.Codicology
{
    /// <summary>
    /// Manuscript units part.
    /// <para>Tag: <c>it.vedph.tgr.ms-units</c>.</para>
    /// </summary>
    [Tag("it.vedph.tgr.ms-units")]
    public sealed class MsUnitsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        public List<MsUnit> Units { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsUnitsPart"/> class.
        /// </summary>
        public MsUnitsPart()
        {
            Units = new List<MsUnit>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c>, <c>palimpsest</c> (true if any),
        /// and a collection of pins with these keys: <c>material</c>,
        /// <c>sheet-count</c>, <c>guard-sheet-count</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem? item)
        {
            DataPinBuilder builder = new();

            builder.Set("tot", Units?.Count ?? 0, false);

            if (Units?.Count > 0)
            {
                bool pal = false;
                foreach (MsUnit unit in Units)
                {
                    if (!pal && unit.Palimpsests?.Count > 0) pal = true;

                    builder.AddValue("material", unit.Material);

                    if (unit.SheetCount > 0)
                        builder.AddValue("sheet-count", unit.SheetCount);

                    if (unit.GuardSheetCount > 0)
                        builder.AddValue("guard-sheet-count", unit.GuardSheetCount);
                }
                builder.AddValue("palimpsest", pal);
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
                    "The total count of units."),
                new DataPinDefinition(DataPinValueType.String,
                    "material",
                    "The units materials.",
                    "M"),
                new DataPinDefinition(DataPinValueType.Boolean,
                    "palimpsest",
                    "True if any unit includes palimpsest(s)."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "sheet-count",
                    "The units sheet counts.",
                    "M"),
                new DataPinDefinition(DataPinValueType.Integer,
                    "guard-sheet-count",
                    "The units guard sheet counts.",
                    "M")
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

            sb.Append("[MsUnits]");

            if (Units?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (MsUnit unit in Units)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(unit);
                }
                if (Units.Count > 3)
                    sb.Append("...(").Append(Units.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
