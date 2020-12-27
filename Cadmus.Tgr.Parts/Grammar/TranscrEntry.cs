using Cadmus.Philology.Parts.Layers;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Tgr.Parts.Grammar
{
    /// <summary>
    /// An entry in a <see cref="TranscrLayerFragment"/>.
    /// </summary>
    public class TranscrEntry
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public ApparatusEntryType Type { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the language(s).
        /// </summary>
        public string[] Languages { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the group identifier.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the sources.
        /// </summary>
        public List<ReadingSource> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranscrEntry"/> class.
        /// </summary>
        public TranscrEntry()
        {
            Sources = new List<ReadingSource>();
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
            sb.Append('[').Append(Type).Append(']');

            if (!string.IsNullOrEmpty(Role))
                sb.Append(' ').Append(Role).Append(' ');

            if (Languages?.Length > 0)
                sb.Append(' ').Append(string.Join(", ", Languages));

            sb.Append(": ").Append(Value);
            return sb.ToString();
        }
    }
}
