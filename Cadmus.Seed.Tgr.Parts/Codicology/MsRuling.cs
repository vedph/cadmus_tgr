namespace Cadmus.Seed.Tgr.Parts.Codicology
{
    /// <summary>
    /// Manuscript's ruling, used in <see cref="MsUnit"/>.
    /// </summary>
    public class MsRuling
    {
        /// <summary>
        /// Gets or sets the manner of execution.
        /// </summary>
        public string Manner { get; set; }

        /// <summary>
        /// Gets or sets the system.
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{Type}] {Manner} - {System}";
        }
    }
}
