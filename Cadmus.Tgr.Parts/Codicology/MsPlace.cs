using Cadmus.Parts;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Tgr.Parts.Codicology
{
    /// <summary>
    /// A place of origin for a manuscript.
    /// </summary>
    public class MsPlace
    {
        /// <summary>
        /// Gets or sets the geographical area. This is the top-level geographical
        /// indication in the hierarchy further specified by <see cref="Address"/>.
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the optional address inside the area. This is a
        /// string including 1 or more components, in hierarchical order,
        /// like the addresses typically used in geocoding systems.
        /// Components are separated by comma. For instance, the area might
        /// be "France", and the address "Lyon, Bibliothéque Civique").
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the site inside the city.
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Gets or sets the rank: 0=not specified, else a value representing
        /// the likelihood of this location hypothesis.
        /// </summary>
        public short Rank { get; set; }

        /// <summary>
        /// Gets or sets the sources for this place identification.
        /// </summary>
        public List<DocReference> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsPlace"/> class.
        /// </summary>
        public MsPlace()
        {
            Sources = new List<DocReference>();
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

            sb.Append("[MsPlace]");

            if (!string.IsNullOrEmpty(Area)) sb.Append(' ').Append(Area);

            if (!string.IsNullOrEmpty(Address))
            {
                if (sb[sb.Length - 1] != ' ') sb.Append(", ");
                sb.Append(Address);
            }

            return sb.ToString();
        }
    }
}
