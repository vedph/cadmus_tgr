using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadmus.Tgr.Parts.Codicology
{
    /// <summary>
    /// Manuscript scripts part.
    /// <para>Tag: <c>it.vedph.tgr.ms-scripts</c>.</para>
    /// </summary>
    [Tag("it.vedph.tgr.ms-scripts")]
    public sealed class MsScriptsPart : PartBase
    {
        public List<MsScript> Scripts { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsScriptsPart"/> class.
        /// </summary>
        public MsScriptsPart()
        {
            Scripts = new List<MsScript>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collection of pins with
        /// these keys: <c>role</c>, <c>language</c>, <c>type</c>,
        /// <c>hand-id</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
        {
            DataPinBuilder builder = new();

            builder.Set("tot", Scripts?.Count ?? 0, false);

            if (Scripts?.Count > 0)
            {
                foreach (MsScript script in Scripts)
                {
                    builder.AddValue("role", script.Role);
                    if (script.Languages?.Count > 0)
                        builder.AddValues("language", script.Languages);
                    builder.AddValue("type", script.Type);
                    if (script.Hands?.Count > 0)
                    {
                        builder.AddValues("hand-id",
                            script.Hands.Select(h => h.Id)!);
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
                    "The total count of scripts."),
                new DataPinDefinition(DataPinValueType.String,
                    "role",
                    "The scripts role.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "language",
                    "The scripts language.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "type",
                    "The scripts type.",
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

            sb.Append("[MsScripts]");

            if (Scripts?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (MsScript script in Scripts)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(script);
                }
                if (Scripts.Count > 3)
                    sb.Append("...(").Append(Scripts.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
