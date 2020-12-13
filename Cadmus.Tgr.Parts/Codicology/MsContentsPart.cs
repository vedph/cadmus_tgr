using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Tgr.Parts.Codicology
{
    /// <summary>
    /// Manuscript contents part.
    /// <para>Tag: <c>it.vedph.tgr.ms-contents</c>.</para>
    /// </summary>
    [Tag("it.vedph.tgr.ms-contents")]
    public sealed class MsContentsPart : PartBase
    {
        /// <summary>
        /// The contents.
        /// </summary>
        public List<MsContent> Contents { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsContentsPart"/> class.
        /// </summary>
        public MsContentsPart()
        {
            Contents = new List<MsContent>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            // tot-count
            builder.Set("tot", Contents?.Count ?? 0, false);

            if (Contents?.Count > 0)
            {
                foreach (var content in Contents)
                {
                    builder.AddValue("work", content.Work);
                    builder.AddValue("title", content.Title,
                        filter: true, filterOptions: true);
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
                    "The total count of contents."),
                new DataPinDefinition(DataPinValueType.String,
                    "work",
                    "The contents works citations.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "title",
                    "The contents titles.",
                    "Mf")
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

            sb.Append("[MsContents]");

            if (Contents?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var content in Contents)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(content);
                }
                if (Contents.Count > 3)
                    sb.Append("...(").Append(Contents.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
