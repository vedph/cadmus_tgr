using System.Collections.Generic;

namespace Cadmus.Tgr.Parts.Codicology
{
    /// <summary>
    /// A manuscript's subscription, used in <see cref="MsHistoryPart"/>.
    /// </summary>
    public class MsSubscription
    {
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the hand identifier.
        /// </summary>
        public string HandId { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the location(s).
        /// </summary>
        public List<MsLocation> Locations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSubscription"/> class.
        /// </summary>
        public MsSubscription()
        {
            Locations = new List<MsLocation>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{Language} - {HandId}] " +
                (Text?.Length > 60? Text.Substring(0, 60) + "..." : Text);
        }
    }
}
