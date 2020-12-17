using System.Collections.Generic;

namespace Cadmus.Tgr.Parts.Codicology
{
    /// <summary>
    /// A script in the <see cref="MsScriptsPart"/>.
    /// </summary>
    public class MsScript
    {
        /// <summary>
        /// Gets or sets the role (e.g. main hand, secondary hand, scriptio
        /// superior, scriptio inferior, etc.).
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the hands.
        /// </summary>
        public List<MsHand> Hands { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsScript"/> class.
        /// </summary>
        public MsScript()
        {
            Hands = new List<MsHand>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{Language}] {Role}: {Type}";
        }
    }
}
