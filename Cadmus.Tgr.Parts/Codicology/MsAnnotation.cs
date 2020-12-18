namespace Cadmus.Tgr.Parts.Codicology
{
    /// <summary>
    /// A manuscript's annotation, used in <see cref="MsHistoryPart"/>.
    /// </summary>
    public class MsAnnotation
    {
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the hand identifier.
        /// </summary>
        public string HandId { get; set; }

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
            return $"[{Language}] {HandId}";
        }
    }
}
