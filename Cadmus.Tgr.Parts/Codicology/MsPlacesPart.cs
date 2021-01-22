using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Tgr.Parts.Codicology
{
    /// <summary>
    /// Manuscript's place(s) of origin part.
    /// <para>Tag: <c>it.vedph.tgr.ms-places</c>.</para>
    /// </summary>
    [Tag("it.vedph.tgr.ms-places")]
    public sealed class MsPlacesPart : PartBase
    {
        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        public List<MsPlace> Places { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsPlacesPart"/> class.
        /// </summary>
        public MsPlacesPart()
        {
            Places = new List<MsPlace>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collection of pins with
        /// these keys: ....</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            builder.Set("tot", Places?.Count ?? 0, false);

            if (Places?.Count > 0)
            {
                foreach (var place in Places)
                {
                    if (!string.IsNullOrEmpty(place.Area))
                    {
                        builder.AddValue("area", place.Area,
                            filter: true, filterOptions: true);
                    }

                    if (!string.IsNullOrEmpty(place.City))
                    {
                        builder.AddValue("city", place.City,
                            filter: true, filterOptions: true);
                    }

                    if (!string.IsNullOrEmpty(place.Address))
                    {
                        builder.AddValue("address", place.Address);

                        int n = 0;
                        foreach (string c in place.Address.Split(','))
                        {
                            builder.AddValue($"address-{++n}", c.Trim(),
                                filter: true, filterOptions: true);
                        }
                    }
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
                   "The total count of entries."),
                new DataPinDefinition(DataPinValueType.String,
                    "area",
                    "The manuscript's area, if any.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "city",
                    "The manuscript's city, if any.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "address",
                    "The manuscript's address, if any.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "address-{N}",
                    "The list of manuscript's address components, "+
                    "each numbered, if any.",
                    "Mf"),
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
            StringBuilder sb = new StringBuilder();

            sb.Append("[MsPlaces]");

            if (Places?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var entry in Places)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(entry);
                }
                if (Places.Count > 3)
                    sb.Append("...(").Append(Places.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
