using System.Collections.Generic;

namespace Cadmus.Tgr.Parts.Grammar
{
    /// <summary>
    /// An annotated tag used in <see cref="LingTaggedForm"/>.
    /// </summary>
    public class AnnotatedTag
    {
        /// <summary>
        /// Gets or sets the tag value.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        public List<TaggedNote> Notes { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnotatedTag"/> class.
        /// </summary>
        public AnnotatedTag()
        {
            Notes = new List<TaggedNote>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Value}: {Notes?.Count ?? 0}";
        }
    }
}
