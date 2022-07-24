using Cadmus.General.Parts;

namespace Cadmus.Tgr.Parts.Codicology
{
    /// <summary>
    /// Description of a manuscript's ornamentation, used in the
    /// <see cref="MsOrnamentsPart"/>.
    /// </summary>
    public class MsOrnament
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the start position in the manuscript.
        /// </summary>
        public MsLocation? Start { get; set; }

        /// <summary>
        /// Gets or sets the end position in the manuscript.
        /// </summary>
        public MsLocation? End { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public PhysicalSize? Size { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{Type}] {Start}-{End}";
        }
    }
}
