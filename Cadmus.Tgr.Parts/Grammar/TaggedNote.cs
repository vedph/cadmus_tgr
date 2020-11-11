namespace Cadmus.Tgr.Parts.Grammar
{
    /// <summary>
    /// A tagged note used in <see cref="AnnotatedTag"/>.
    /// </summary>
    public class TaggedNote
    {
        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Tag}: {(Note?.Length > 80? Note.Substring(0, 80) + "..." : Note)}";
        }
    }
}
